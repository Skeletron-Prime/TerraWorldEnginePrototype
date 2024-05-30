using System.Numerics;
using TerraWorldEnginePrototype.Core.Mathematics;
using TerraWorldEnginePrototype.Graphics;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    internal class GLRenderer : Renderer
    {
        private readonly GLProgram program;
        private readonly VertexArray vertexArray;
        private readonly GLBuffer<Vertex> vertexBuffer;

        private readonly int modelLocation;
        private readonly int viewLocation;
        private readonly int projectionLocation;

        public GLRenderer()
        {
            program = new GLProgram();
            vertexArray = new VertexArray();
            vertexBuffer = new GLBuffer<Vertex>();

            program.AddShader(ShaderType.VertexShader, "PlatformIndependence\\Rendering\\OpenGL\\Shaders\\VertexShader3D.glsl");
            program.AddShader(ShaderType.FragmentShader, "PlatformIndependence\\Rendering\\OpenGL\\Shaders\\FragmentShader3D.glsl");

            program.Build();

            program.Use();

            modelLocation = GL.GetUniformLocation(program.Id, "model");
            viewLocation = GL.GetUniformLocation(program.Id, "view");
            projectionLocation = GL.GetUniformLocation(program.Id, "projection");
        }


        public override void Draw(EngineObject eo)
        {
            UploadMesh(eo.Mesh);

            vertexArray.Bind();

            Model(Matrix4x4.CreateFromQuaternion(eo.Transform.Rotation) * Matrix4x4.CreateTranslation(eo.Transform.Position) * Matrix4x4.CreateScale(eo.Transform.Scale));

            GL.DrawArrays(DrawMode.Triangles, 0, eo.Mesh.Vertices.Length);
        }

        private void UploadMesh(Mesh mesh)
        {
            if (!mesh.IsChanged)
                return;

            if (mesh.VertexCount == 0)
                throw new Exception("Mesh has no vertices!");

            var vertices = new Vertex[mesh.VertexCount];

            for (int i = 0; i < mesh.Vertices.Length; i++)
            {
                vertices[i] = new Vertex
                {
                    Location = mesh.Vertices[i],
                };

                if (mesh.HasColors)
                    vertices[i].Color = mesh.Colors[i];
            }

            vertexBuffer.BufferData(vertices, BufferType.ArrayBuffer, BufferUsage.StaticDraw);

            vertexArray.AddAttribute3f();
            vertexArray.AddAttribute4f();

            vertexArray.Build(vertexBuffer);

            vertexArray.Bind();

            mesh.IsChanged = false;
        }

        #region MVP Matrix

        public override void Model(Matrix4x4 model)
        {
            GL.UniformMatrix4(modelLocation, false, ref model);
        }

        public override void View(Matrix4x4 view)
        {
            GL.UniformMatrix4(viewLocation, false, ref view);
        }

        public override void Projection(Matrix4x4 projection)
        {
            GL.UniformMatrix4(projectionLocation, false, ref projection);
        }

        #endregion

        private struct Vertex
        {
            public Vector3 Location;
            public Color Color;
        }
    }
}