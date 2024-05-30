using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager.WinWnd;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    internal class GLDevice : GraphicsDevice
    {
        private GLExtension? extension;

        public GLDevice(Window window)
        {
            InitExtensions(window);
            SetViewport((int)window.Settings.Size.X, (int)window.Settings.Size.Y);
        }

        public override void Clear(bool color = true, bool depth = true)
        {
            GL.Clear((color ? ClearBufferMask.ColorBufferBit : 0) | (depth ? ClearBufferMask.DepthBufferBit : 0));
        }

        public override void Clear(float r = 0, float g = 0, float b = 0, float a = 1, bool color = true, bool depth = true)
        {
            GL.ClearColor(r, g, b, a);

            GL.Clear((color ? ClearBufferMask.ColorBufferBit : 0) | (depth ? ClearBufferMask.DepthBufferBit : 0));
        }

        public override void SetViewport(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
        }

        public override void Dispose()
        {
            extension!.Dispose();
        }

        private void InitExtensions(Window window)
        {
            if (window is WindowsWindow windowsWindow)
            {
                extension = new WGL(windowsWindow);
            }
            else
            {
                throw new PlatformNotSupportedException("Windows only!");
            }
        }
    }
}
