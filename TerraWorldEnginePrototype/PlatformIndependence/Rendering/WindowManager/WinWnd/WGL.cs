﻿using System.Runtime.InteropServices;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager.WinWnd
{
    internal class WGL
    {
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

        private WindowsWindow window;

        /// <summary>
        /// The handle to the OpenGL rendering context.
        /// </summary>
        private nint hglrc;

        internal void Dispose()
        {
            wglMakeCurrent(window.hdc, nint.Zero);
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
