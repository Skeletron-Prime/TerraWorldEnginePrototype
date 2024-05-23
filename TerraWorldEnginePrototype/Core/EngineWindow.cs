using System.Diagnostics;
using System.Numerics;
using TerraWorldEnginePrototype.Core.Mathematics;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager;

namespace TerraWorldEnginePrototype.Core
{
    public class EngineWindow : NativeWindow
    {
        Mesh mesh;
        GLRenderer renderer;

        Stopwatch stopwatch = new Stopwatch();

        int Width;
        int Height;

        public EngineWindow(WindowSettings windowSettings) : base(windowSettings)
        {
            renderer = new GLRenderer();

            Width = (int)windowSettings.Size.X;
            Height = (int)windowSettings.Size.Y;

            // create a cube 
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

        protected override void OnLoad()
        {
            stopwatch.Start();

            GL.Enable(EnableCap.DepthTest);

            base.OnLoad();
        }

        protected override void OnRender()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            renderer.Draw(mesh);

            Matrix4x4 model = Matrix4x4.CreateRotationZ(-(float)stopwatch.Elapsed.TotalSeconds) * Matrix4x4.CreateRotationY((float)stopwatch.Elapsed.TotalSeconds) * Matrix4x4.CreateRotationX((float)stopwatch.Elapsed.TotalSeconds);

            Matrix4x4 view = Matrix4x4.CreateLookAt(new Vector3(4f, 4f, 4f), new Vector3(0, 0, 0), Vector3.UnitY);

            Matrix4x4 projection = Matrix4x4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), Width / Height, 0.1f, 100.0f);

            renderer.Model(ref model);

            renderer.View(ref view);

            renderer.Projection(ref projection);

            base.OnRender();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            Width = e.Width;
            Height = e.Height;

            GL.Viewport(0, 0, Width, Height);

            base.OnResize(e);
        }
    }
}
