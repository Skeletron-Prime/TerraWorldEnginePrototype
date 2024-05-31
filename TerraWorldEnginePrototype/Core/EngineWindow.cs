using TerraWorldEnginePrototype.Graphics;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager;

namespace TerraWorldEnginePrototype.Core
{
    public class EngineWindow : NativeWindow
    {
        public EngineWindow(WindowSettings windowSettings, Input input) : base(windowSettings, input)
        {
        }

        protected override void OnLoad()
        {
            GameObject cube = new CubeObject(new Transform());

            Scene.AddObject(cube);

            base.OnLoad();
        }
    }
}
