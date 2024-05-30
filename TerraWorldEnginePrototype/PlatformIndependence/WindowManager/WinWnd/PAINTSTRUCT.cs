using System.Runtime.InteropServices;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager.WinWnd
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct PAINTSTRUCT
    {
        internal nint hdc;
        internal bool fErase;
        internal RECT rcPaint;
        internal bool fRestore;
        internal bool fIncUpdate;
        internal byte[] rgbReserved;
    }
}
