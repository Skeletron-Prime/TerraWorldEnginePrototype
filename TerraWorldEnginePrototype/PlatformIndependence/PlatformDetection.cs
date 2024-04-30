using System.Runtime.InteropServices;

namespace TerraWorldEnginePrototype.PlatformIndependence
{
    /// <summary>
    /// Provides a set of methods to detect the current platform.
    /// </summary>
    public static class PlatformDetection
    {
        /// <summary>
        /// Returns true if the current platform is Windows, otherwise returns false.
        /// </summary>
        public static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        /// <summary>
        /// Returns true if the current platform is Linux, otherwise returns false.
        /// </summary>
        public static bool IsLinux => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        /// <summary>
        /// Returns true if the current platform is macOS, otherwise returns false.
        /// </summary>
        public static bool IsMacOS => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        /// <summary>
        /// Returns true if the current platform is FreeBSD, otherwise returns false.
        /// </summary>
        public static bool IsFreeBSD => RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD);
    }
}