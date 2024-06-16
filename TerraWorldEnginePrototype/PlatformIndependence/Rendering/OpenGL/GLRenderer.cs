using System.Numerics;
using TerraWorldEnginePrototype.Core;
using TerraWorldEnginePrototype.Core.Mathematics;
using TerraWorldEnginePrototype.Graphics;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    internal class GLRenderer : Renderer
    {
        private readonly GLProgram program;
        private readonly BufferManager bufferManager;

        private readonly int modelLocation;
        private readonly int viewLocation;
        private readonly int projectionLocation;
        
        private readonly int lightPositionLocation;
        private readonly int lightColorLocation;
        private readonly int viewPositionLocation;

        public GLRenderer()
        {
            program = new GLProgram();
            bufferManager = new BufferManager();

            program.AddShader(ShaderType.VertexShader, "PlatformIndependence\\Rendering\\OpenGL\\Shaders\\VertexShader3D.glsl");
            program.AddShader(ShaderType.FragmentShader, "PlatformIndependence\\Rendering\\OpenGL\\Shaders\\FragmentShader3D.glsl");

            program.Build();

            program.Use();

            modelLocation = GL.GetUniformLocation(program.Id, "model");
            viewLocation = GL.GetUniformLocation(program.Id, "view");
            projectionLocation = GL.GetUniformLocation(program.Id, "projection");

            lightPositionLocation = GL.GetUniformLocation(program.Id, "lightPosition");
            lightColorLocation = GL.GetUniformLocation(program.Id, "lightColor");
            viewPositionLocation = GL.GetUniformLocation(program.Id, "viewPosition");

            GL.Enable(EnableCap.DepthTest);
        }

        public override void DrawScene(Scene scene)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            foreach (var engineObject in scene.SceneGraph)
            {
                if (engineObject.Value is GameObject gameObject)
                {
                    Draw(gameObject);
                }
                else if (engineObject.Value is PointLight pointLight)
                {
                    GL.Uniform3f(lightPositionLocation, pointLight.Position.X, pointLight.Position.Y, pointLight.Position.Z);
                    GL.Uniform3f(lightColorLocation, pointLight.Color.R, pointLight.Color.G, pointLight.Color.B);
                }
            }

            GL.Uniform3f(viewPositionLocation, scene.CurrentCamera.Transform.Position.X, scene.CurrentCamera.Transform.Position.Y, scene.CurrentCamera.Transform.Position.Z);

            View(scene.CurrentCamera.ViewMatrix);
            Projection(scene.CurrentCamera.ProjectionMatrix);
        }

        private void Draw(GameObject gameObject)
        {
            UploadMesh(gameObject.Mesh);

            Model(gameObject.Transform.GetModelMatrix());

            GL.DrawElements(DrawMode.Triangles, gameObject.Mesh.IndexCount, DataType.UnsignedInt, 0);
        }

        private void UploadMesh(Mesh mesh)
        {
            if (bufferManager.Contains(mesh))
            {
                var (vertexArrayObject, _, indexBufferObject) = bufferManager.GetBuffer(mesh);

                vertexArrayObject.Bind();
                indexBufferObject.Bind(BufferType.ElementArrayBuffer);

                return;
            }

            var (vertexArray, vertexBuffer, indexBuffer) = bufferManager.GetBuffer(mesh);

            if (mesh.VertexCount == 0)
                throw new Exception("Mesh has no vertices!");

            if (mesh.IndexCount == 0)
                throw new Exception("Mesh has no indices!");

            var vertices = new Vertex[mesh.VertexCount];

            for (int i = 0; i < mesh.Vertices.Length; i++)
            {
                vertices[i] = new Vertex
                {
                    Location = mesh.Vertices[i],
                };

                if (mesh.HasNormals)
                {
                    vertices[i].Normal = mesh.Normals[i];
                }

                if (mesh.HasColors)
                {
                    vertices[i].Color = mesh.Colors[i];
                }
                else
                {
                    vertices[i].Color = Color.White;
                }
            }

            vertexBuffer.BufferData(vertices, BufferType.ArrayBuffer, BufferUsage.StaticDraw);

            indexBuffer.BufferData(mesh.Indices, BufferType.ElementArrayBuffer, BufferUsage.StaticDraw);

            vertexArray.AddAttribute3f();
            vertexArray.AddAttribute3f();
            vertexArray.AddAttribute4f();

            vertexArray.Build(vertexBuffer);

            vertexArray.Bind();

            indexBuffer.Bind(BufferType.ElementArrayBuffer);
        }

        #region MVP Matrix

        protected override void Model(Matrix4x4 model)
        {
            GL.UniformMatrix4(modelLocation, false, ref model);
        }

        protected override void View(Matrix4x4 view)
        {
            GL.UniformMatrix4(viewLocation, false, ref view);
        }

        protected override void Projection(Matrix4x4 projection)
        {
            GL.UniformMatrix4(projectionLocation, false, ref projection);
        }

        #endregion

        public struct Vertex
        {
            public Vector3 Location;
            public Vector3 Normal;
            public Color Color;
        }
    }

    internal class BufferManager
    {
        private readonly Dictionary<Mesh, (VertexArray, GLBuffer<GLRenderer.Vertex>, GLBuffer<uint>)> buffers = [];

        public (VertexArray, GLBuffer<GLRenderer.Vertex>, GLBuffer<uint>) GetBuffer(Mesh mesh)
        {
            if (buffers.ContainsKey(mesh))
                return buffers[mesh];

            var vertexArray = new VertexArray();
            var vertexBuffer = new GLBuffer<GLRenderer.Vertex>();
            var indexBuffer = new GLBuffer<uint>();

            buffers.Add(mesh, (vertexArray, vertexBuffer, indexBuffer));

            return (vertexArray, vertexBuffer, indexBuffer);
        }

        public bool Contains(Mesh mesh)
        {
            return buffers.ContainsKey(mesh);
        }
    }
}