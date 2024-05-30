using System.Runtime.InteropServices;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager.WinWnd
{
    /// <summary>
    /// The Win32 class provides essential functionality for low-level interaction with the Windows operating system,
    /// including defining window styles and messages, managing window creation and destruction, handling device contexts,
    /// and facilitating message processing and window updates. It serves as a bridge between managed C# code and 
    /// the unmanaged Windows API.
    /// </summary>

    internal class Win32
    {
        #region Windows Parameters

        // Class Style Constants

        /// <summary>
        /// Redraws the entire window if a movement or size adjustment changes the width of the client area.
        /// </summary>
        internal const uint CS_HREDRAW = 0x0002;
        
        /// <summary>
        /// Redraws the entire window if a movement or size adjustment changes the height of the client area.
        /// </summary>
        internal const uint CS_VREDRAW = 0x0001;

        // Window Messages

        /// <summary>
        /// Sent as a signal that a window or an application should terminate.
        /// </summary>
        internal const uint WM_CLOSE = 0x0010;

        /// <summary>
        /// Sent when the system or another application makes a request to paint a portion of an application's window.
        /// </summary>
        internal const uint WM_PAINT = 0x000F;

        /// <summary>
        /// Sent to a window after its size has changed.
        /// </summary>
        internal const uint WM_SIZE = 0x0005;

        /// <summary>
        /// Posted to the window with the keyboard focus when a nonsystem key is pressed.
        /// </summary>
        internal const uint WM_KEYDOWN = 0x0100;

        /// <summary>
        /// Posted to the window with the keyboard focus when a nonsystem key is released.
        /// </summary>
        internal const uint WM_KEYUP = 0x0101;

        /// <summary>
        /// Posted to a window when the cursor moves.
        /// </summary>
        internal const uint WM_MOUSEMOVE = 0x0200;

        // Window Styles

        /// <summary>
        /// Standard overlapped window.
        /// </summary>
        internal const uint WS_OVERLAPPEDWINDOW = 0x00CF;

        /// <summary>
        /// Window is initially visible.
        /// </summary>
        internal const uint WS_VISIBLE = 0x10000000;

        /// <summary>
        /// Window has a window menu on its title bar.
        /// </summary>
        internal const uint WS_SYSMENU = 0x00080000;

        /// <summary>
        /// Window has a minimize button.
        /// </summary>
        internal const uint WS_MINIMIZEBOX = 0x00020000;

        /// <summary>
        /// Window has a maximize button.
        /// </summary>
        internal const uint WS_MAXIMIZEBOX = 0x00010000;

        /// <summary>
        /// Window has a title bar (includes the WS_BORDER style).
        /// </summary>
        internal const uint WS_CAPTION = 0x00C00000;

        /// <summary>
        /// Window has a sizing border.
        /// </summary>
        internal const uint WS_THICKFRAME = 0x00040000;

        /// <summary>
        /// Forces a top-level window onto the taskbar when the window is visible.
        /// </summary>
        internal const uint WS_EX_APPWINDOW = 0x40000;

        // Show Window Commands

        /// <summary>
        /// Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position.
        /// </summary>
        internal const int SW_SHOWNORMAL = 1;

        /// <summary>
        /// Activates the window and displays it as a minimized window.
        /// </summary>
        internal const int SW_SHOWMINIMIZED = 2;

        /// <summary>
        /// Activates the window and displays it as a maximized window.
        /// </summary>
        internal const int SW_SHOWMAXIMIZED = 3;

        /// <summary>
        /// Activates the window and displays it in its current size and position.
        /// </summary>
        internal const int SW_SHOW = 5;

        #endregion

        #region user32.dll

        /// <summary>
        /// Prepares the specified window for painting and fills a PAINTSTRUCT structure with information about the painting.
        /// </summary>
        /// <param name="hWnd">A handle to the window to be painted.</param>
        /// <param name="lpPaint">A pointer to a PAINTSTRUCT structure that will receive painting information.</param>
        /// <returns>A handle to a display device context for the specified window.</returns>
        [DllImport("user32.dll")]
        internal static extern nint BeginPaint(nint hWnd, out PAINTSTRUCT lpPaint);

        /// <summary>
        /// Creates an overlapped, pop-up, or child window with an extended window style.
        /// </summary>
        /// <param name="dwExStyle">The extended window style of the window being created.</param>
        /// <param name="lpClassName">A null-terminated string or a class atom created by a previous call to the RegisterClass or RegisterClassEx function.</param>
        /// <param name="lpWindowName">The window name.</param>
        /// <param name="dwStyle">The style of the window being created.</param>
        /// <param name="x">The initial horizontal position of the window.</param>
        /// <param name="y">The initial vertical position of the window.</param>
        /// <param name="nWidth">The width of the window.</param>
        /// <param name="nHeight">The height of the window.</param>
        /// <param name="hWndParent">A handle to the parent or owner window of the window being created.</param>
        /// <param name="hMenu">A handle to a menu, or specifies a child-window identifier.</param>
        /// <param name="hInstance">A handle to the instance of the module to be associated with the window.</param>
        /// <param name="lpParam">Pointer to a value to be passed to the window through the CREATESTRUCT structure.</param>
        /// <returns>If the function succeeds, the return value is a handle to the new window. If the function fails, the return value is NULL.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern nint CreateWindowEx(uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int nWidth, int nHeight, nint hWndParent, nint hMenu, nint hInstance, nint lpParam);

        /// <summary>
        /// Calls the default window procedure to provide default processing for any window messages that an application does not process.
        /// </summary>
        /// <param name="hWnd">A handle to the window procedure that received the message.</param>
        /// <param name="uMsg">The message.</param>
        /// <param name="wParam">Additional message information. The content of this parameter depends on the value of the uMsg parameter.</param>
        /// <param name="lParam">Additional message information. The content of this parameter depends on the value of the uMsg parameter.</param>
        /// <returns>The return value is the result of the message processing and depends on the message sent.</returns>
        [DllImport("user32.dll")]
        internal static extern nint DefWindowProc(nint hWnd, uint uMsg, nint wParam, nint lParam);

        /// <summary>
        /// Destroys the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window to be destroyed.</param>
        /// <returns>If the function succeeds, the return value is true. If the function fails, the return value is false.</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyWindow(nint hWnd);

        /// <summary>
        /// Dispatches a message to a window procedure.
        /// </summary>
        /// <param name="lpmsg">A pointer to an MSG structure that contains the message.</param>
        /// <returns>The return value specifies the value returned by the window procedure.</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DispatchMessage([In] ref MSG lpmsg);

        /// <summary>
        /// Marks the end of painting in the specified window. This function is required for each call to the BeginPaint function, but only after painting is complete.
        /// </summary>
        /// <param name="hWnd">A handle to the window that has been repainted.</param>
        /// <param name="lpPaint">A pointer to a PAINTSTRUCT structure that contains the painting information retrieved by BeginPaint.</param>
        /// <returns>If the function succeeds, the return value is true.</returns>
        [DllImport("user32.dll")]
        internal static extern nint EndPaint(nint hWnd, [In] ref PAINTSTRUCT lpPaint);

        /// <summary>
        /// Retrieves the coordinates of a window's client area.
        /// </summary>
        /// <param name="hWnd">A handle to the window whose client coordinates are to be retrieved.</param>
        /// <param name="lpRect">A pointer to a RECT structure that receives the client coordinates.</param>
        /// <returns>If the function succeeds, the return value is true. If the function fails, the return value is false.</returns>
        [DllImport("user32.dll")]
        internal static extern nint GetClientRect(nint hWnd, out RECT lpRect);

        /// <summary>
        /// Retrieves a handle to a display device context (DC) for the client area of a specified window or for the entire screen.
        /// </summary>
        /// <param name="hWnd">A handle to the window whose DC is to be retrieved.</param>
        /// <returns>If the function succeeds, the return value is a handle to the DC for the specified window's client area. If the function fails, the return value is NULL.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern nint GetDC(nint hWnd);

        /// <summary>
        /// Adds the specified rectangle to the specified window's update region. The update region represents the portion of the window's client area that must be redrawn.
        /// </summary>
        /// <param name="hWnd">A handle to the window whose update region has changed.</param>
        /// <param name="lpRect">A pointer to a RECT structure that contains the client coordinates of the rectangle to be added to the update region. If this parameter is NULL, the entire client area is added to the update region.</param>
        /// <param name="bErase">Specifies whether the background within the update region is to be erased.</param>
        /// <returns>If the function succeeds, the return value is true. If the function fails, the return value is false.</returns>
        [DllImport("user32.dll")]
        internal static extern bool InvalidateRect(nint hWnd, nint lpRect, [MarshalAs(UnmanagedType.Bool)] bool bErase);

        /// <summary>
        /// Checks the message queue for a posted message and retrieves it, if one exists.
        /// </summary>
        /// <param name="lpMsg">A pointer to an MSG structure that receives message information from the thread's message queue.</param>
        /// <param name="hWnd">A handle to the window whose messages are to be retrieved.</param>
        /// <param name="wMsgFilterMin">The value of the first message in the range of messages to be examined.</param>
        /// <param name="wMsgFilterMax">The value of the last message in the range of messages to be examined.</param>
        /// <param name="wRemoveMsg">Specifies how messages are to be handled.</param>
        /// <returns>If a message is available, the return value is true. If no messages are available, the return value is false.</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PeekMessage(out MSG lpMsg, nint hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

        /// <summary>
        /// Registers a window class for subsequent use in calls to the CreateWindow or CreateWindowEx function.
        /// </summary>
        /// <param name="lpWndClass">A pointer to a WNDCLASS structure.</param>
        /// <returns>If the function succeeds, the return value is a class atom that uniquely identifies the class being registered. If the function fails, the return value is zero.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern ushort RegisterClass([In] ref WNDCLASS lpWndClass);

        /// <summary>
        /// Releases a device context (DC), freeing it for use by other applications. The effect of ReleaseDC depends on the type of device context.
        /// </summary>
        /// <param name="hWnd">A handle to the window whose DC is to be released.</param>
        /// <param name="hdc">A handle to the DC to be released.</param>
        /// <returns>The return value indicates whether the DC was released. If the DC was released, the return value is true. If the DC was not released, the return value is false.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ReleaseDC(nint hWnd, nint hdc);

        /// <summary>
        /// Sets the specified window's show state.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="nCmdShow">Controls how the window is to be shown.</param>
        /// <returns>If the window was previously visible, the return value is true. If the window was previously hidden, the return value is false.</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ShowWindow(nint hWnd, int nCmdShow);

        /// <summary>
        /// Translates virtual-key messages into character messages. The character messages are posted to the calling thread's message queue, to be read the next time the thread retrieves a message from the queue.
        /// </summary>
        /// <param name="lpMsg">A pointer to an MSG structure that contains message information retrieved from the calling thread's message queue.</param>
        /// <returns>If the message is translated (that is, a character message is posted to the thread's message queue), the return value is true. If the message is not translated (that is, a character message is not posted to the thread's message queue), the return value is false.</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool TranslateMessage([In] ref MSG lpMsg);

        /// <summary>
        /// Updates the client area of the specified window by sending a WM_PAINT message to the window if the window's update region is not empty.
        /// </summary>
        /// <param name="hWnd">A handle to the window to be updated.</param>
        /// <returns>If the function succeeds, the return value is true. If the function fails, the return value is false.</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UpdateWindow(nint hWnd);

        #endregion

        #region gdi32.dll

        /// <summary>
        /// Attempts to match an appropriate pixel format supported by a device context to a given pixel format specification.
        /// </summary>
        /// <param name="hdc">Handle to the device context.</param>
        /// <param name="ppfd">Pointer to a PIXELFORMATDESCRIPTOR structure that specifies the requested pixel format.</param>
        /// <returns>If the function succeeds, the return value is a one-based index of the closest pixel format match in the device context's pixel format table. If the function fails, the return value is zero.</returns>
        [DllImport("gdi32.dll", SetLastError = true)]
        internal static extern int ChoosePixelFormat(nint hdc, ref PIXELFORMATDESCRIPTOR ppfd);

        /// <summary>
        /// Sets the pixel format of the specified device context to the format specified by the iPixelFormat index.
        /// </summary>
        /// <param name="hdc">Handle to the device context.</param>
        /// <param name="iPixelFormat">Index that specifies the pixel format.</param>
        /// <param name="ppfd">Pointer to a PIXELFORMATDESCRIPTOR structure that contains the logical pixel format specification.</param>
        /// <returns>If the function succeeds, the return value is true. If the function fails, the return value is false.</returns>
        [DllImport("gdi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetPixelFormat(nint hdc, int iPixelFormat, ref PIXELFORMATDESCRIPTOR ppfd);

        /// <summary>
        /// Exchanges the front and back buffers if the current pixel format for the window supports double-buffering.
        /// </summary>
        /// <param name="hdc">Handle to the device context.</param>
        /// <returns>If the function succeeds, the return value is true. If the function fails, the return value is false.</returns>
        [DllImport("gdi32.dll", SetLastError = true)]
        internal static extern int SwapBuffers(nint hdc);

        #endregion

        #region shcore.dll

        /// <summary>
        /// Sets the process-default DPI awareness level. This affects how Windows scales the UI of the process.
        /// </summary>
        /// <param name="value">The DPI awareness value to set.</param>
        /// <returns>If the function succeeds, the return value is true. If the function fails, the return value is false.</returns>
        [DllImport("shcore.dll", SetLastError = true)]
        internal static extern bool SetProcessDpiAwareness(PROCESS_DPI_AWARENESS value);

        #endregion
    }
}
