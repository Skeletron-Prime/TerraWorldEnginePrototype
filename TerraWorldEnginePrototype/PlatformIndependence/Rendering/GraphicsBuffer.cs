using TerraWorldEnginePrototype.PlatformIndependence.Rendering.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering
{
    public abstract class GraphicsBuffer : IDisposable
    {
        protected abstract bool IsDisposed { get; }
        public abstract void Dispose();
    }
}
