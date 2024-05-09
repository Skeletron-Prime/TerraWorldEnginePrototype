using System.Numerics;
using TerraWorldEnginePrototype.Core.Mathematics;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.Primitives;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager;

namespace TerraWorldEnginePrototype.Core
{
    /// <summary>
    /// StartUp and ShutDown module of the TerraWorldEngine.
    /// </summary>
    public class StartUpAndShutDuwnModule : Object
    {
        static Window? window;

        static Mesh mesh => new Mesh()
        {
            // make me please a rectangle
            Vertices =
            [
                new Vector3(-0.5f, -0.5f, 0.0f),
                new Vector3(0.5f, -0.5f, 0.0f),
                new Vector3(0.5f, 0.5f, 0.0f),
                new Vector3(-0.5f, 0.5f, 0.0f)
            ],

            Indices =
            [
                0, 1, 2,
                2, 3, 0
            ]
        };

        static float[] textureCoordinates =
        [
            0.0f, 0.0f,
            1.0f, 0.0f,
            1.0f, 1.0f,
            0.0f, 1.0f
        ];

        static GLTexture texture = GLTexture.LoadFromFile("E:\\Coding\\GameDev\\TestGame\\Test\\Textures\\TreeTexture.bmp", TextureType.Texture2D);

        public static void StartUp()
        {
            window = Window.Create(new WindowSettings());

            GL.Viewport(0, 0, 800, 600);

            GL.ClearColor(0f, 0f, 0f, 1.0f);

            window.Show();

            while (window.IsVisible)
            {
                GL.Clear(ClearBufferMask.ColorBufferBit);

                Renderer.DrawMesh(mesh, texture, textureCoordinates);

                window.PoolEvents();

                window.SwapBuffers();
            }
        }

        public static void ShutDown()
        {
            window!.Dispose();
        }
    }
}
