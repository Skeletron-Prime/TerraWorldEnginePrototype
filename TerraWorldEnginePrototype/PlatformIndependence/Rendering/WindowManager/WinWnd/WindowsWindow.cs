using System.Runtime.InteropServices;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager.WinWnd
{
    public class WindowsWindow : Window
    {
        const uint CS_HREDRAW = 0x0002;
        const uint CS_VREDRAW = 0x0001;
        const uint WM_CLOSE = 0x0010;
        const uint WM_PAINT = 0x000F;
        const uint WS_OVERLAPPEDWINDOW = 0x00CF;
        const uint WS_VISIBLE = 0x10000000;
        const uint WS_SYSMENU = 0x00080000;
        const uint WS_MINIMIZEBOX = 0x00020000;
        const uint WS_MAXIMIZEBOX = 0x00010000;
        const uint WS_CAPTION = 0x00C00000;
        const uint WS_EX_APPWINDOW = 0x40000;
        const int SW_SHOWNORMAL = 1;

        nint hwnd;

        public override bool IsVisible => isVisible;

        internal nint hdc;
        bool isVisible = true;

        GraphicsDevice graphicsDevice;

        private readonly WndProc windowProcDelegate;

        public WindowsWindow(WindowSettings windowSettings)
        {
            windowProcDelegate = WindowProc;

            WNDCLASS wc = new WNDCLASS
            {
                style = CS_HREDRAW | CS_VREDRAW,
                lpfnWndProc = Marshal.GetFunctionPointerForDelegate(windowProcDelegate),
                hInstance = Marshal.GetHINSTANCE(typeof(WindowsWindow).Module),
                hbrBackground = nint.Zero,
                lpszClassName = windowSettings.Title
            };

            RegisterClass(ref wc);

            hwnd = CreateWindowEx(WS_EX_APPWINDOW, wc.lpszClassName, windowSettings.Title, WS_OVERLAPPEDWINDOW | WS_VISIBLE | WS_SYSMENU | WS_MINIMIZEBOX | WS_MAXIMIZEBOX, windowSettings.XLocation, windowSettings.YLocation, windowSettings.Width, windowSettings.Height, nint.Zero, nint.Zero, wc.hInstance, nint.Zero);

            hdc = GetDC(hwnd);

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

            int iPixelFormat = ChoosePixelFormat(hdc, ref pfd);
            SetPixelFormat(hdc, iPixelFormat, ref pfd);

            graphicsDevice = new GLDevice(this);
        }

        public override void Show()
        {
            ShowWindow(hwnd, SW_SHOWNORMAL);
            UpdateWindow(hwnd);
        }

        public override void PoolEvents()
        {
            while (PeekMessage(out MSG msg, nint.Zero, 0, 0, 1))
            {
                TranslateMessage(ref msg);
                DispatchMessage(ref msg);
            }
        }

        public override void SwapBuffers()
        {
            _ = SwapBuffers(hdc);
        }

        public override void Dispose()
        {
            graphicsDevice.Dispose();
            DestroyWindow(hwnd);
        }

        nint WindowProc(nint hWnd, uint uMsg, nint wParam, nint lParam)
        {
            switch (uMsg)
            {
                case WM_CLOSE:
                    PostQuitMessage(0);
                    return nint.Zero;
                //case WM_PAINT:
                //TextOut(hdc, 0, 0, "Hello, Windows!", 14);
                //return IntPtr.Zero;
                default:
                    return DefWindowProc(hWnd, uMsg, wParam, lParam);
            }
        }

        void PostQuitMessage(int exitCode)
        {
            DestroyWindow(hwnd);
            isVisible = false;
        }

        [DllImport("user32.dll")]
        static extern nint DefWindowProc(nint hWnd, uint uMsg, nint wParam, nint lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern ushort RegisterClass([In] ref WNDCLASS lpWndClass);

        [DllImport("user32.dll", SetLastError = true)]
        static extern nint CreateWindowEx(uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int nWidth, int nHeight, nint hWndParent, nint hMenu, nint hInstance, nint lpParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(nint hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool UpdateWindow(nint hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool PeekMessage(out MSG lpMsg, nint hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool TranslateMessage([In] ref MSG lpMsg);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool DispatchMessage([In] ref MSG lpmsg);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool DestroyWindow(nint hWnd);

        [DllImport("gdi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetPixelFormat(nint hdc, int iPixelFormat, ref PIXELFORMATDESCRIPTOR ppfd);

        [DllImport("gdi32.dll", SetLastError = true)]
        static extern int ChoosePixelFormat(nint hdc, ref PIXELFORMATDESCRIPTOR ppfd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern nint GetDC(nint hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ReleaseDC(nint hWnd, nint hdc);

        [DllImport("gdi32.dll", SetLastError = true)]
        static extern int SwapBuffers(nint hdc);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate nint WndProc(nint hWnd, uint uMsg, nint wParam, nint lParam);
    }
}
