namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering
{
    public abstract class GraphicsProgram : IDisposable
    {
        protected abstract bool IsDisposed { get; }
        public abstract void Dispose();
    }
}
