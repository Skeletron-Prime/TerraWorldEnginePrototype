using System.Numerics;
using TerraWorldEnginePrototype.Core.Mathematics;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager;

namespace TerraWorldEnginePrototype.Core
{
    /// <summary>
    /// StartUp and ShutDown module of the TerraWorldEngine.
    /// </summary>
    public class StartUpAndShutDuwnModule : Object
    {
        public static void StartUp()
        {
            WindowSettings windowSettings = new WindowSettings();
            windowSettings.Size = new Vector2(1280, 720);

            EngineWindow engineWindow = new EngineWindow(windowSettings);
            engineWindow.Run();
        }

        public static void ShutDown()
        {
        }
    }

    public class EngineWindow : NativeWindow
    {
        Mesh mesh;

        public EngineWindow(WindowSettings windowSettings) : base(windowSettings)
        {
            GLProgram program = new GLProgram();
            program.AddShader(ShaderType.VertexShader, "E:\\Coding\\GameEngine\\Prototype\\TerraWorldEnginePrototype\\PlatformIndependence\\Rendering\\OpenGL\\Shaders\\shader.vert");
            program.AddShader(ShaderType.FragmentShader, "E:\\Coding\\GameEngine\\Prototype\\TerraWorldEnginePrototype\\PlatformIndependence\\Rendering\\OpenGL\\Shaders\\shader.frag");

            program.Build();

            // make me please a mesh for rectangle
            mesh = new Mesh(
            [
                new Vector3(-0.5f, -0.5f, 0.0f),
                new Vector3(0.5f, -0.5f, 0.0f),
                new Vector3(0.5f, 0.5f, 0.0f),
                new Vector3(0.5f, 0.5f, 0.0f),
                new Vector3(-0.5f, 0.5f, 0.0f),
                new Vector3(-0.5f, -0.5f, 0.0f)
            ]);
        }

        public override void OnLoad()
        {
            VertexArray vertexArray = new VertexArray();

            GLBuffer<Vector3> vertexBuffer = new GLBuffer<Vector3>();
            vertexBuffer.Bind(BufferType.ArrayBuffer);
            vertexBuffer.BufferData(mesh.Vertices, BufferType.ArrayBuffer, BufferUsage.StaticDraw);

            vertexArray.AddAttribute3f();
            vertexArray.Build(vertexBuffer);

            vertexArray.Bind();

            base.OnLoad();
        }

        public override void OnRender()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.DrawArrays(DrawMode.Triangles, 0, mesh.Vertices.Length);

            base.OnRender();
        }
    }
}
