using System.Runtime.InteropServices;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager.WinWnd
{
    internal class WGL : GLExtension
    {
        /// <summary>
        /// The handle to the OpenGL rendering context.
        /// </summary>
        private nint hglrc;

        private readonly WindowsWindow? window;

        internal WGL(Window window)
        {
            this.window = window as WindowsWindow;

            if (this.window == null)
            {
                throw new Exception("Windows only!");
            }

            hglrc = wglCreateContext(this.window.hdc);
            wglMakeCurrent(this.window.hdc, hglrc);

            wglSwapIntervalEXT = Marshal.GetDelegateForFunctionPointer<wglExtSwapInterval>(wglGetProcAddress("wglSwapIntervalEXT"));

            wglSwapIntervalEXT(1);
        }

        public override void Dispose()
        {
            wglMakeCurrent(window!.hdc, nint.Zero);
            wglDeleteContext(hglrc);
        }

        [DllImport("opengl32.dll", SetLastError = true)]
        internal static extern nint wglCreateContext(nint hdc);

        [DllImport("opengl32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool wglMakeCurrent(nint hdc, nint hglrc);

        [DllImport("opengl32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool wglDeleteContext(nint hglrc);

        [DllImport("opengl32.dll", SetLastError = true)]
        internal static extern nint wglGetProcAddress(string lpszProc);

        delegate void wglExtSwapInterval(int interval);
        wglExtSwapInterval wglSwapIntervalEXT;
    }
}
