using TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager.WinWnd;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    internal class GLGraphicsDevice : GraphicsDevice
    {
        private readonly WGL wgl;

        internal GLGraphicsDevice(Window window)
        {
            wgl = new WGL(window);
        }

        public override void Dispose()
        {
            wgl.Dispose();
        }
    }
}
