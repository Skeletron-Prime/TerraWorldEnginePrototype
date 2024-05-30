using System.Numerics;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager;

namespace TerraWorldEnginePrototype.Core
{
    /// <summary>
    /// StartUp and ShutDown module of the TerraWorldEngine.
    /// </summary>
    public class StartUpAndShutDuwnModule : Test_EngineObject
    {
        public static void StartUp()
        {
            WindowSettings windowSettings = new()
            {
                Location = new Vector2(0, 0),
                Size = new Vector2(3840, 1600)
            };

            EngineInput input = new();

            EngineWindow engineWindow = new(windowSettings, input);
            engineWindow.Run();
        }

        public static void ShutDown()
        {
        }
    }
}
