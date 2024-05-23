using System.Runtime.InteropServices;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager.WinWnd;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager
{
    public abstract class Window
    {
        public WindowSettings Settings { get; set; }

        public Action? OnPaint;

        public WindowSizeCallback? OnSizeCallback;

        public delegate void WindowSizeCallback(int width, int height);

        public Window(WindowSettings windowSettings)
        {
            Settings = windowSettings;
        }

        public static Window Create(WindowSettings windowSettings)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new WindowsWindow(windowSettings);
            }
            else
            {
                throw new PlatformNotSupportedException("Windows only!");
            }
        }

        public abstract void Show();

        /// <summary>
        /// Pools for window events.
        /// </summary>
        public abstract void PoolEvents();

        public abstract void SwapBuffers();

        public abstract void Dispose();

        public void SetWindowSizeCallback(WindowSizeCallback callback)
        {
            OnSizeCallback = callback;
        }
    }
}
