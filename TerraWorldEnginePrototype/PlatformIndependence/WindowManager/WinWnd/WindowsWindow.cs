using System.Numerics;
using System.Runtime.InteropServices;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager.WinWnd
{
    public class WindowsWindow : Window
    {
        private nint hwnd;
        private WGL wgl;
        private readonly WndProc windowProcDelegate;

        public nint hdc;

        public WindowsWindow(WindowSettings settings) : base(settings)
        {
            windowProcDelegate = WindowProc;

            WNDCLASS wc = new WNDCLASS
            {
                style = Win32.CS_HREDRAW | Win32.CS_VREDRAW,
                lpfnWndProc = Marshal.GetFunctionPointerForDelegate(windowProcDelegate),
                hInstance = Marshal.GetHINSTANCE(typeof(WindowsWindow).Module),
                hbrBackground = nint.Zero,
                lpszClassName = settings.Title
            };

            Win32.RegisterClass(ref wc);

            hwnd = Win32.CreateWindowEx(Win32.WS_EX_APPWINDOW, wc.lpszClassName, settings.Title, Win32.WS_OVERLAPPEDWINDOW | Win32.WS_VISIBLE | Win32.WS_SYSMENU | Win32.WS_MINIMIZEBOX | Win32.WS_MAXIMIZEBOX | Win32.WS_THICKFRAME, (int)settings.Location.X, (int)settings.Location.Y, (int)settings.Size.X, (int)settings.Size.Y, nint.Zero, nint.Zero, wc.hInstance,nint.Zero);

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
                case Win32.WM_SIZE:
                    int width = (int)(lParam & 0xFFFF);
                    int height = (int)((lParam >> 16) & 0xFFFF);
                    OnResize(width, height);
                    return nint.Zero;

                default:
                    return Win32.DefWindowProc(hWnd, uMsg, wParam, lParam);
            }
        }

        void PostQuitMessage(int exitCode)
        {
            Win32.DestroyWindow(hwnd);
            Settings.IsVisible = false;
        }

        private void OnResize(int width, int height)
        {
            Settings.Size = new Vector2(width, height);
            OnSizeCallback?.Invoke(width, height);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate nint WndProc(nint hWnd, uint uMsg, nint wParam, nint lParam);
    }
}
