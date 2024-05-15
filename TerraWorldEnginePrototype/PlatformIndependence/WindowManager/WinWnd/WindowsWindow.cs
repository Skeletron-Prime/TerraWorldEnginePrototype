using System.Runtime.InteropServices;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager.WinWnd
{
    public class WindowsWindow : Window
    {
        nint hwnd;

        public override bool IsVisible => isVisible;

        internal nint hdc;
        bool isVisible = true;

        WGL wgl;

        private readonly WndProc windowProcDelegate;

        static WindowsWindow()
        {
        }

        public WindowsWindow(WindowSettings windowSettings)
        {
            windowProcDelegate = WindowProc;

            WNDCLASS wc = new WNDCLASS
            {
                style = Win32.CS_HREDRAW | Win32.CS_VREDRAW,
                lpfnWndProc = Marshal.GetFunctionPointerForDelegate(windowProcDelegate),
                hInstance = Marshal.GetHINSTANCE(typeof(WindowsWindow).Module),
                hbrBackground = nint.Zero,
                lpszClassName = windowSettings.Title
            };

            Win32.RegisterClass(ref wc);

            hwnd = Win32.CreateWindowEx
                (
                Win32.WS_EX_APPWINDOW, 
                wc.lpszClassName, 
                windowSettings.Title, 
                Win32.WS_OVERLAPPEDWINDOW | 
                Win32.WS_VISIBLE | 
                Win32.WS_SYSMENU | 
                Win32.WS_MINIMIZEBOX | 
                Win32.WS_MAXIMIZEBOX, 
                (int)windowSettings.Location.X, 
                (int)windowSettings.Location.Y, 
                (int)windowSettings.Size.X, 
                (int)windowSettings.Size.Y, 
                nint.Zero, 
                nint.Zero, 
                wc.hInstance, 
                nint.Zero
                );

            hdc = Win32.GetDC(hwnd);

            PIXELFORMATDESCRIPTOR pfd = new PIXELFORMATDESCRIPTOR
            {
                nSize = (ushort)Marshal.SizeOf(typeof(PIXELFORMATDESCRIPTOR)),
                nVersion = 1,
                dwFlags = (uint)(DWFlags.PFD_DRAW_TO_WINDOW | DWFlags.PFD_SUPPORT_OPENGL | DWFlags.PFD_DOUBLEBUFFER),
                iPixelType = (byte)DWFlags.PFD_TYPE_RGBA,
                cColorBits = 32,
                cDepthBits = 24,
                cStencilBits = 8
            };

            int iPixelFormat = Win32.ChoosePixelFormat(hdc, ref pfd);
            Win32.SetPixelFormat(hdc, iPixelFormat, ref pfd);

            wgl = new WGL(this);
        }

        public override void Show()
        {
            Win32.ShowWindow(hwnd, Win32.SW_SHOWNORMAL);
            Win32.UpdateWindow(hwnd);
        }

        public override void PoolEvents()
        {
            while (Win32.PeekMessage(out MSG msg, nint.Zero, 0, 0, 1))
            {
                Win32.TranslateMessage(ref msg);
                Win32.DispatchMessage(ref msg);
            }
        }

        public override void SwapBuffers()
        {
            _ = Win32.SwapBuffers(hdc);
        }

        public override void Dispose()
        {
            wgl.Dispose();
            Win32.DestroyWindow(hwnd);
        }

        nint WindowProc(nint hWnd, uint uMsg, nint wParam, nint lParam)
        {
            switch (uMsg)
            {
                case Win32.WM_CLOSE:
                    PostQuitMessage(0);
                    return nint.Zero;
                //case WM_PAINT:
                //TextOut(hdc, 0, 0, "Hello, Windows!", 14);
                //return IntPtr.Zero;
                default:
                    return Win32.DefWindowProc(hWnd, uMsg, wParam, lParam);
            }
        }

        void PostQuitMessage(int exitCode)
        {
            Win32.DestroyWindow(hwnd);
            isVisible = false;
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate nint WndProc(nint hWnd, uint uMsg, nint wParam, nint lParam);
    }
}
