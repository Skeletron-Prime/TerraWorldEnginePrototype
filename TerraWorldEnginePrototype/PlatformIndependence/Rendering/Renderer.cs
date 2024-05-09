using System.Numerics;
using TerraWorldEnginePrototype.Core.Mathematics;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering
{
    public class Renderer
    {
        private static readonly GLBuffer<Vector3> vertexBufferObject;
        private static readonly GLBuffer<uint> indexBufferObject;
        private static readonly GLVertexArray vertexArrayObject;

        static GLProgram program;
        static Renderer()
        {
            Dictionary<ShaderType, string> shadersources = new Dictionary<ShaderType, string>
            {
                { ShaderType.VertexShader, File.ReadAllText("E:\\Coding\\GameEngine\\Prototype\\TerraWorldEnginePrototype\\PlatformIndependence\\Rendering\\OpenGL\\Shaders\\shader.vert") },
                { ShaderType.FragmentShader, File.ReadAllText("E:\\Coding\\GameEngine\\Prototype\\TerraWorldEnginePrototype\\PlatformIndependence\\Rendering\\OpenGL\\Shaders\\shader.frag") }
            };

            program = new GLProgram(shadersources);

            vertexBufferObject = new GLBuffer<Vector3>(BufferTarget.ArrayBuffer);
            indexBufferObject = new GLBuffer<uint>(BufferTarget.ElementArrayBuffer);
            vertexArrayObject = new GLVertexArray();
        }

        public static void DrawMesh(Mesh mesh)
        {
            int vertexColorLocation = GL.GetUniformLocation(program.ID, "u_Color");
            GL.Uniform4f(vertexColorLocation, 1.0f, 1.0f, 1.0f, 1.0f);

            vertexBufferObject.BufferData(mesh.Vertices, BufferUsage.StaticDraw);

            GL.VertexAttribPointer(0, 3, DataType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            indexBufferObject.BufferData(mesh.Indices, BufferUsage.StaticDraw);

            GL.DrawElements(DrawMode.TriangleStrip, mesh.Indices.Length, DataType.UnsignedInt, 0);
        }

        public static void DrawMesh(Mesh mesh, GLTexture texture, float[] textureCoordinates)
        {
            int vertexColorLocation = GL.GetUniformLocation(program.ID, "u_Color");
            GL.Uniform4f(vertexColorLocation, 1.0f, 1.0f, 1.0f, 1.0f);

            vertexBufferObject.BufferData(mesh.Vertices, BufferUsage.StaticDraw);

            GL.VertexAttribPointer(0, 3, DataType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GLBuffer<float> textureBuffer = new GLBuffer<float>(BufferTarget.ArrayBuffer);
            textureBuffer.BufferData(textureCoordinates, BufferUsage.StaticDraw);

            GL.VertexAttribPointer(1, 2, DataType.Float, false, 2 * sizeof(float), 0);
            GL.EnableVertexAttribArray(1);

            indexBufferObject.BufferData(mesh.Indices, BufferUsage.StaticDraw);

            texture.Bind();

            GL.DrawElements(DrawMode.TriangleStrip, mesh.Indices.Length, DataType.UnsignedInt, 0);
        }
    }
}
