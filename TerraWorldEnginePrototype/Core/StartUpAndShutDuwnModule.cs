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
        GLRenderer renderer;

        public EngineWindow(WindowSettings windowSettings) : base(windowSettings)
        {
            renderer = new GLRenderer();

            // create a triangle
            mesh = new Mesh(
            [
                new Vector3(-0.5f, -0.5f, 0.0f),  // bottom left
                new Vector3(0.5f, -0.5f, 0.0f),   // bottom right
                new Vector3(0.0f, 0.5f, 0.0f)     // top
            ]);

            mesh.Colors =
            [
                Color.Red,
                Color.Green,
                Color.Blue
            ];
        }

        public override void OnLoad()
        {
            base.OnLoad();
        }

        public override void OnRender()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            renderer.Draw(mesh);

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
