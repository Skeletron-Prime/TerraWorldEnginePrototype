namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager
{
    public abstract class NativeWindow
    {
        private Window window;

        public event Action Load;
        public event Action Update;
        public event Action Render;
        public event Action Unload;

        public NativeWindow(WindowSettings settings)
        {
            window = Window.Create(settings);
            window.Show();
        }

        public void Run()
        {
            OnLoad();
            while (window.IsVisible)
            {
                OnUpdate();
                OnRender();

                window.PoolEvents();

                window.SwapBuffers();
            }
            OnUnload();
        }

        public virtual void OnLoad()
        {
            Load?.Invoke();
        }

        public virtual void OnUpdate()
        {
            Update?.Invoke();
        }

        public virtual void OnRender()
        {
            Render?.Invoke();
        }

        public virtual void OnUnload()
        {
            Unload?.Invoke();
        }

        public void Dispose()
        {
            window.Dispose();
        }
    }
}
