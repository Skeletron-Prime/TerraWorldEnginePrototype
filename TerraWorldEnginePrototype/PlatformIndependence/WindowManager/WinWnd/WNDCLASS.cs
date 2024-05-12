using System.Runtime.InteropServices;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager.WinWnd
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct WNDCLASS
    {
        public uint style;
        public nint lpfnWndProc;
        public int cbClsExtra;
        public int cbWndExtra;
        public nint hInstance;
        public nint hIcon;
        public nint hCursor;
        public nint hbrBackground;
        public string lpszMenuName;
        public string lpszClassName;
    }
}
