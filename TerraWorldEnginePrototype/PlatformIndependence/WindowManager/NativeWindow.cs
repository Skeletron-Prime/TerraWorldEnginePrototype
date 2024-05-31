using TerraWorldEnginePrototype.Core;
using TerraWorldEnginePrototype.Graphics;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager
{
    public abstract class NativeWindow
    {
        private readonly Window window;

        protected Scene Scene { get; set; } = new(new Camera());

        private readonly Renderer render;

        public event Action? Load;
        public event Action? Update;
        public event Action? Render;
        public event Action? Unload;

        public NativeWindow(WindowSettings settings, Input input)
        {
            window = Window.Create(settings, input);
            window.Show();

            render = Renderer.Create();
        }

        public void Run()
        {
            OnLoad();
            while (window.Settings.IsVisible)
            {
                OnUpdate();
                OnRender();

                render.DrawScene(Scene);

                window.PoolEvents();
                window.SwapBuffers();
            }
            OnUnload();
        }

        /// <summary>
        /// Gets called when the window is loaded. 
        /// </summary>
        protected virtual void OnLoad()
        {
            Load?.Invoke();
        }

        /// <summary>
        /// Gets called each frame. Is called before the window is rendered.
        /// </summary>
        protected virtual void OnUpdate()
        {
            Update?.Invoke();
        }

        /// <summary>
        /// Gets called each time the window is rendered.
        /// </summary>
        protected virtual void OnRender()
        {
            Render?.Invoke();
        }

        /// <summary>
        /// Gets called when the window is closed.
        /// </summary>
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
