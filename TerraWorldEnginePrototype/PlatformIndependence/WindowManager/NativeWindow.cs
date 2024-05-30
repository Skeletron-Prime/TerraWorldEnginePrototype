using TerraWorldEnginePrototype.Core;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager
{
    public abstract class NativeWindow
    {
        private readonly Window window;

        public event Action? Load;
        public event Action? Update;
        public event Action? Render;
        public event Action? Unload;

        public NativeWindow(WindowSettings settings, Input input)
        {
            window = Window.Create(settings, input);
            window.Show();
        }

        public void Run()
        {
            OnLoad();
            while (window.Settings.IsVisible)
            {
                OnUpdate();
                OnRender();

                window.PoolEvents();

                window.SwapBuffers();
            }
            OnUnload();
        }

        protected virtual void OnLoad()
        {
            Load?.Invoke();
        }

        protected virtual void OnUpdate()
        {
            Update?.Invoke();
        }

        protected virtual void OnRender()
        {
            Render?.Invoke();
        }

        protected virtual void OnUnload()
        {
            Unload?.Invoke();
        }

        public void Dispose()
        {
            window.Dispose();
        }
    }
}
