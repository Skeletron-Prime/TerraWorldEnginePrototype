namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager.WinWnd
{
    /// <summary>
    /// DPI awareness levels. Effects how the window is scaled on high DPI displays.
    /// </summary>
    internal enum PROCESS_DPI_AWARENESS
    {
        /// <summary>
        /// DPI unaware. The window is not scaled on high DPI displays.
        /// </summary>
        PROCESS_DPI_UNAWARE = 0,

        /// <summary>
        /// System DPI aware. The window is scaled by the system on high DPI displays.
        /// </summary>
        PROCESS_SYSTEM_DPI_AWARE = 1,

        /// <summary>
        /// Per monitor DPI aware. The window is scaled by the system on high DPI displays.
        /// </summary>
        PROCESS_PER_MONITOR_DPI_AWARE = 2
    }
}
