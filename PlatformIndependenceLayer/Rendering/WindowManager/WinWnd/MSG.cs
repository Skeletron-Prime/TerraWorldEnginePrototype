using System.Drawing;
using System.Runtime.InteropServices;

namespace PlatformIndependenceLayer.Rendering.WindowManager.WinWnd
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct MSG
    {
        public nint hwnd;
        public uint message;
        public nint wParam;
        public nint lParam;
        public uint time;
        public Point pt;
    }
}
