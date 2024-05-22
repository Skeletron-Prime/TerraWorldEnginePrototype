using System.Diagnostics;
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
            windowSettings.Size = new Vector2(1280, 1280);

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
        GLRenderer renderer;

        Stopwatch stopwatch = new Stopwatch();

        public EngineWindow(WindowSettings windowSettings) : base(windowSettings)
        {
            renderer = new GLRenderer();

            // create a triangle
            mesh = new Mesh(
            [
    // Front face
new Vector3(-1.0f, -1.0f,  1.0f),
new Vector3( 1.0f, -1.0f,  1.0f),
new Vector3( 1.0f,  1.0f,  1.0f),
new Vector3( 1.0f,  1.0f,  1.0f),
new Vector3(-1.0f,  1.0f,  1.0f),
new Vector3(-1.0f, -1.0f,  1.0f),

    // Back face
new Vector3(-1.0f, -1.0f, -1.0f),
new Vector3(-1.0f,  1.0f, -1.0f),
new Vector3( 1.0f,  1.0f, -1.0f),
new Vector3( 1.0f,  1.0f, -1.0f),
new Vector3( 1.0f, -1.0f, -1.0f),
new Vector3(-1.0f, -1.0f, -1.0f),

    // Left face
new Vector3(-1.0f,  1.0f,  1.0f),
new Vector3(-1.0f,  1.0f, -1.0f),
new Vector3(-1.0f, -1.0f, -1.0f),
new Vector3(-1.0f, -1.0f, -1.0f),
new Vector3(-1.0f, -1.0f,  1.0f),
new Vector3(-1.0f,  1.0f,  1.0f),

    // Right face
new Vector3( 1.0f,  1.0f,  1.0f),
new Vector3( 1.0f, -1.0f, -1.0f),
new Vector3( 1.0f,  1.0f, -1.0f),
new Vector3( 1.0f, -1.0f, -1.0f),
new Vector3( 1.0f,  1.0f,  1.0f),
new Vector3( 1.0f, -1.0f,  1.0f),

    // Top face
new Vector3(-1.0f,  1.0f,  1.0f),
new Vector3( 1.0f,  1.0f,  1.0f),
new Vector3( 1.0f,  1.0f, -1.0f),
new Vector3( 1.0f,  1.0f, -1.0f),
new Vector3(-1.0f,  1.0f, -1.0f),
new Vector3(-1.0f,  1.0f,  1.0f),

    // Bottom face
new Vector3(-1.0f, -1.0f,  1.0f),
new Vector3(-1.0f, -1.0f, -1.0f),
new Vector3( 1.0f, -1.0f, -1.0f),
new Vector3( 1.0f, -1.0f, -1.0f),
new Vector3( 1.0f, -1.0f,  1.0f),
new Vector3(-1.0f, -1.0f,  1.0f)
            ]);

            mesh.Colors =
            [
                // Front face
                Color.Red,
                Color.Red,
                Color.Red,
                Color.Red,
                Color.Red,
                Color.Red,

                // Back face
                Color.Green,
                Color.Green,
                Color.Green,
                Color.Green,
                Color.Green,
                Color.Green,

                // Left face
                Color.Blue,
                Color.Blue,
                Color.Blue,
                Color.Blue,
                Color.Blue,
                Color.Blue,

                // Right face
                Color.Yellow,
                Color.Yellow,
                Color.Yellow,
                Color.Yellow,
                Color.Yellow,
                Color.Yellow,

                // Top face
                Color.Purple,
                Color.Purple,
                Color.Purple,
                Color.Purple,
                Color.Purple,
                Color.Purple,

                // Bottom face
                Color.Cyan,
                Color.Cyan,
                Color.Cyan,
                Color.Cyan,
                Color.Cyan,
                Color.Cyan
            ];
        }

        public override void OnLoad()
        {
            stopwatch.Start();

            GL.Enable(EnableCap.DepthTest);

            base.OnLoad();
        }

        public override void OnRender()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            renderer.Draw(mesh);

            Matrix4x4 model = Matrix4x4.CreateRotationZ((float)stopwatch.Elapsed.TotalSeconds * 40);

            Matrix4x4 view = Matrix4x4.CreateLookAt(new Vector3(0f, 0f, 5f), Vector3.Zero, Vector3.UnitY);

            int Width = 1280;
            int Height = 1280;

            Matrix4x4 projection = Matrix4x4.CreatePerspectiveFieldOfView(Mathematics.MathF.DegreesToRadians(45), Width / Height, 0.1f, 100.0f);

            renderer.Model(ref model);

            renderer.View(ref view);

            renderer.Projection(ref projection);

            base.OnRender();
        }
    }

    public struct Vertex
    {
        public Vector3 Position;
        public Color Color;

        public Vertex(Vector3 position, Color color)
        {
            Position = position;
            Color = color;
        }
    }
}
