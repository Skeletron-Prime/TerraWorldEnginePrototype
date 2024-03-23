using CoreEnginePrototype;
using PlatformIndependenceLayer.Rendering.WindowManager.WinWnd;

namespace TerraWorldEnginePrototype
{
    public class TerraWorldEngine
    {
        public static void Main()
        {
            StartUpAndShutDuwnModule.StartUp();

            StartUpAndShutDuwnModule.ShutDown();
        }
    }
}