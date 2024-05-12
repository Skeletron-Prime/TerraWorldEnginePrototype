using System.Numerics;
using TerraWorldEnginePrototype.Core.Mathematics;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager;

namespace TerraWorldEnginePrototype.Core
{
    /// <summary>
    /// StartUp and ShutDown module of the TerraWorldEngine.
    /// </summary>
    public class StartUpAndShutDuwnModule : Object
    {
        static Window? window;

        public static void StartUp()
        {
            window = Window.Create(new WindowSettings());

            GL.Viewport(0, 0, 800, 600);

            GL.ClearColor(0f, 0f, 0f, 1.0f);

            window.Show();

            while (window.IsVisible)
            {
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
