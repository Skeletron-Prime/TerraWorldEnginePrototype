using System.Numerics;
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
            windowSettings.Location = new Vector2(0, 0);
            windowSettings.Size = new Vector2(3840, 1600);

            EngineWindow engineWindow = new EngineWindow(windowSettings);
            engineWindow.Run();
        }

        public static void ShutDown()
        {
        }
    }
}
