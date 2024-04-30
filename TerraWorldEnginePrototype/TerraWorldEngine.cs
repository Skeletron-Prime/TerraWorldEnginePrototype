using TerraWorldEnginePrototype.Core;

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