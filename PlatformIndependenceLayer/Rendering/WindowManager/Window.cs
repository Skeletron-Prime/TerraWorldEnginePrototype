using PlatformIndependenceLayer.Rendering.WindowManager.WinWnd;

namespace PlatformIndependenceLayer.Rendering.WindowManager
{
    public abstract class Window
    {
        public static Window Create(WindowSettings windowSettings)
        {
            if (PlatformDetection.IsWindows)
            {
                return new WindowsWindow(windowSettings);
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
        }

        public abstract void Show();

        /// <summary>
        /// Pools for window events.
        /// </summary>
        public abstract void PoolEvents();

        public abstract void SwapBuffers();

        public abstract void Dispose();

        public abstract bool IsVisible { get; }
    }
}
