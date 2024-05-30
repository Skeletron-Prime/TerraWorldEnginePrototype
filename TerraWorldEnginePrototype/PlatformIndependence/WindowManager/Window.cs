using System.Runtime.InteropServices;
using TerraWorldEnginePrototype.Core;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager.WinWnd;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager
{
    internal abstract class Window
    {
        internal WindowSettings Settings { get; set; }

        internal Input Input { get; set; }

        internal Action? OnPaint;

        internal abstract GraphicsDevice GraphicsDevice { get; set; }

        internal Window(WindowSettings windowSettings, Input input)
        {
            Settings = windowSettings;
            Input = input;
        }

        internal static Window Create(WindowSettings windowSettings, Input input)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new WindowsWindow(windowSettings, input);
            }
            else
            {
                throw new PlatformNotSupportedException("Windows only!");
            }
        }

        internal abstract void Show();

        /// <summary>
        /// Pools for window events.
        /// </summary>
        internal abstract void PoolEvents();

        internal abstract void SwapBuffers();

        internal abstract void Dispose();

        internal abstract void Invalidate();
    }
}
