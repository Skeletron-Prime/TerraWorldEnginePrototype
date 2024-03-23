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

        internal abstract void Show();

        /// <summary>
        /// Pools for window events.
        /// </summary>
        internal abstract void PoolEvents();

        internal abstract void SwapBuffers();

        internal abstract void Dispose();

        internal abstract bool IsVisible { get; }
    }
}
