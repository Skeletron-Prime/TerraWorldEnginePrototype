using System.Runtime.InteropServices;
using TerraWorldEnginePrototype.Core;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager.WinWnd
{
    internal class WindowsWindow : Window
    {
        internal nint hdc;

        private readonly nint hwnd;
        private readonly WndProc wndProc;
        private GraphicsDevice graphicsDevice;

        internal override GraphicsDevice GraphicsDevice
        {
            get => graphicsDevice; 
            set
            {
                graphicsDevice = value;
                graphicsDevice.SetViewport((int)Settings.Size.X, (int)Settings.Size.Y);
            }
        }

        static WindowsWindow()
        {
            Win32.SetProcessDpiAwareness(PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE);
        }

        internal WindowsWindow(WindowSettings settings, Input input) : base(settings, input)
        {
            wndProc = WindowProc;

            WNDCLASS wc = new WNDCLASS
            {
                style = Win32.CS_HREDRAW | Win32.CS_VREDRAW,
                lpfnWndProc = Marshal.GetFunctionPointerForDelegate(wndProc),
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

            graphicsDevice = new GLDevice(this);
        }

        internal override void Show()
        {
            Win32.ShowWindow(hwnd, Win32.SW_SHOWNORMAL);
            Win32.UpdateWindow(hwnd);
        }

        internal override void PoolEvents()
        {
            while (Win32.PeekMessage(out MSG msg, nint.Zero, 0, 0, 1))
            {
                Win32.TranslateMessage(ref msg);
                Win32.DispatchMessage(ref msg);
            }
        }

        internal override void SwapBuffers()
        {
            _ = Win32.SwapBuffers(hdc);
        }

        internal override void Dispose()
        {
            graphicsDevice.Dispose();
            Win32.DestroyWindow(hwnd);
        }

        internal override void Invalidate()
        {
            Win32.InvalidateRect(hwnd, IntPtr.Zero, true);
        }

        nint WindowProc(nint hWnd, uint uMsg, nint wParam, nint lParam)
        {
            switch (uMsg)
            {
                case Win32.WM_CLOSE:
                    PostQuitMessage(0);
                    return nint.Zero;

                case Win32.WM_PAINT:
                    Win32.BeginPaint(hWnd, out PAINTSTRUCT ps);
                    OnPaint?.Invoke();
                    Win32.EndPaint(hWnd, ref ps);
                    return IntPtr.Zero;

                // TODO: Implement the rest of the cases
                case Win32.WM_SIZE:
                    return nint.Zero;

                case Win32.WM_KEYDOWN:
                    return nint.Zero;

                case Win32.WM_KEYUP:
                    return nint.Zero;

                case Win32.WM_MOUSEMOVE:
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

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate nint WndProc(nint hWnd, uint uMsg, nint wParam, nint lParam);
    }
}
