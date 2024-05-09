using TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager.WinWnd;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    internal class GLDevice : GraphicsDevice
    {
        private readonly WGL wgl;

        public override bool IsDisposed { get; protected set; }

        internal GLDevice(Window window)
        {
            wgl = new WGL(window);
        }

        public override void Dispose()
        {
            if (!IsDisposed)
            {
                wgl.Dispose();
                IsDisposed = true;
            }
        }
    }
}
