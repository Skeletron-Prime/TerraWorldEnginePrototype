using PlatformIndependenceLayer.Rendering;
using PlatformIndependenceLayer.Rendering.WindowManager;

namespace CoreEnginePrototype
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

    public class Config : Object
    {
        public Config()
        {
        }

        public void Configurate()
        {
        }

        public void Save()
        {
        }
    }
}
