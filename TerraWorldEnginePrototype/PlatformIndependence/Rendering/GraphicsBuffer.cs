namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering
{
    public abstract class GraphicsBuffer : IDisposable
    {
        public abstract bool IsDisposed { get; protected set; }
        public abstract void Dispose();
    }
}
