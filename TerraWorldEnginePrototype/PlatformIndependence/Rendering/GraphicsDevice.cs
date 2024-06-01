namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering
{
    public abstract class GraphicsDevice
    {
        public abstract void SetViewport(int width, int height);
        public abstract void Dispose();
    }
}
