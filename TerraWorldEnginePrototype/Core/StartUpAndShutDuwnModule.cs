using System.Numerics;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL;
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
        public EngineWindow(WindowSettings windowSettings) : base(windowSettings)
        {
        }
    }
}
