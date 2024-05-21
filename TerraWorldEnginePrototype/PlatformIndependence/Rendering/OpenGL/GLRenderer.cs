using System.Numerics;
using TerraWorldEnginePrototype.Core.Mathematics;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    public class GLRenderer : Renderer
    {
        private readonly GLProgram program;
        private readonly VertexArray vertexArray;
        private readonly GLBuffer<Vertex> vertexBuffer;

        private int modelLocation;
        private int viewLocation;
        private int projectionLocation;

        public GLRenderer()
        {
            program = new GLProgram();
            vertexArray = new VertexArray();
            vertexBuffer = new GLBuffer<Vertex>();

            program.AddShader(ShaderType.VertexShader, "E:\\Coding\\GameEngine\\Prototype\\TerraWorldEnginePrototype\\PlatformIndependence\\Rendering\\OpenGL\\Shaders\\VertexShader3D.glsl");
            program.AddShader(ShaderType.FragmentShader, "E:\\Coding\\GameEngine\\Prototype\\TerraWorldEnginePrototype\\PlatformIndependence\\Rendering\\OpenGL\\Shaders\\FragmentShader3D.glsl");

            program.Build();

            program.Use();

            modelLocation = GL.GetUniformLocation(program.Id, "model");
            viewLocation = GL.GetUniformLocation(program.Id, "view");
            projectionLocation = GL.GetUniformLocation(program.Id, "projection");
        }


        public override void Draw(Mesh mesh)
        {
            UploadMesh(mesh);

            vertexArray.Bind();

            GL.DrawArrays(DrawMode.Triangles, 0, mesh.Vertices.Length);
        }

        private void UploadMesh(Mesh mesh)
        {
            if (!mesh.IsChanged)
            {
                return;
            }

            var vertices = new Vertex[mesh.Vertices.Length];

            for (int i = 0; i < mesh.Vertices.Length; i++)
            {
                vertices[i] = new Vertex
                {
                    Location = mesh.Vertices[i],
                    // if color is not null or empty
                    Color = mesh.Colors != null && mesh.Colors.Length > i ? mesh.Colors[i] : new Color { R = 1, G = 1, B = 1, A = 1 }
                };
            }

            vertexBuffer.BufferData(vertices, BufferType.ArrayBuffer, BufferUsage.StaticDraw);

            vertexArray.AddAttribute3f();

            if (mesh.Colors != null)
            {
                vertexArray.AddAttribute4f();
            }

            vertexArray.Build(vertexBuffer);

            mesh.IsChanged = false;
        }

        public override void Model(ref Matrix4x4 model)
        {
            GL.UniformMatrix4(modelLocation, false, ref model);
        }

        public override void View(ref Matrix4x4 view)
        {
            GL.UniformMatrix4(viewLocation, false, ref view);
        }

        public override void Projection(ref Matrix4x4 projection)
        {
            GL.UniformMatrix4(projectionLocation, false, ref projection);
        }

        private struct Vertex
        {
            public Vector3 Location;
            public Color Color;
        }
    }
}