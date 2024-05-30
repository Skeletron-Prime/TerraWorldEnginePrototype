using System.Runtime.InteropServices;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager.WinWnd
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct RECT
    {
        internal int left, top, right, bottom;
    }
}
