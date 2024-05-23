namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager
{
    public abstract class NativeWindow
    {
        private Window window;

        public event Action? Load;
        public event Action? Update;
        public event Action? Render;
        public event Action? Unload;

        public event Action<ResizeEventArgs>? Resize;

        private Window.WindowSizeCallback? _windowSizeCallBack;

        public NativeWindow(WindowSettings settings)
        {
            window = Window.Create(settings);
            window.Show();

            RegisterWindowCallbacks();
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

        protected virtual void OnResize(ResizeEventArgs e)
        {
            Resize?.Invoke(e);
        }

        public void Dispose()
        {
            window.Dispose();
        }

        private void WindowSizeCallBack(int width, int height)
        {
            OnResize(new ResizeEventArgs(width, height));
        }

        private void RegisterWindowCallbacks()
        {
            _windowSizeCallBack = WindowSizeCallBack;

            window.SetWindowSizeCallback(_windowSizeCallBack);
        }
    }
}
