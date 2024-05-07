namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering
{
    public abstract class GraphicsVertexArray : IDisposable
    {
        protected abstract bool IsDisposed { get; }
        public abstract void Dispose();
    }

    public abstract class GraphicsShader : IDisposable
    {
        protected abstract bool IsDisposed { get; }
        public abstract void Dispose();
    }
}
