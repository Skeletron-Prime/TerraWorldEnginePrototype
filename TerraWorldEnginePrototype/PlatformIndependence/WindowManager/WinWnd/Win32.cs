using System.Runtime.InteropServices;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager.WinWnd
{
    internal class Win32
    {
        #region Windows Parameters

        internal const uint CS_HREDRAW = 0x0002;
        internal const uint CS_VREDRAW = 0x0001;
        internal const uint WM_CLOSE = 0x0010;
        internal const uint WM_PAINT = 0x000F;
        internal const uint WS_OVERLAPPEDWINDOW = 0x00CF;
        internal const uint WS_VISIBLE = 0x10000000;
        internal const uint WS_SYSMENU = 0x00080000;
        internal const uint WS_MINIMIZEBOX = 0x00020000;
        internal const uint WS_MAXIMIZEBOX = 0x00010000;
        internal const uint WS_CAPTION = 0x00C00000;
        internal const uint WS_EX_APPWINDOW = 0x40000;
        internal const int SW_SHOWNORMAL = 1;

        #endregion

        #region user32.dll

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern nint CreateWindowEx(uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int nWidth, int nHeight, nint hWndParent, nint hMenu, nint hInstance, nint lpParam);

        [DllImport("user32.dll")]
        internal static extern nint DefWindowProc(nint hWnd, uint uMsg, nint wParam, nint lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyWindow(nint hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DispatchMessage([In] ref MSG lpmsg);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern nint GetDC(nint hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PeekMessage(out MSG lpMsg, nint hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern ushort RegisterClass([In] ref WNDCLASS lpWndClass);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ReleaseDC(nint hWnd, nint hdc);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ShowWindow(nint hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool TranslateMessage([In] ref MSG lpMsg);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UpdateWindow(nint hWnd);

        #endregion

        [DllImport("gdi32.dll", SetLastError = true)]
        internal static extern int ChoosePixelFormat(nint hdc, ref PIXELFORMATDESCRIPTOR ppfd);

        [DllImport("gdi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetPixelFormat(nint hdc, int iPixelFormat, ref PIXELFORMATDESCRIPTOR ppfd);

        [DllImport("gdi32.dll", SetLastError = true)]
        internal static extern int SwapBuffers(nint hdc);
    }
}
