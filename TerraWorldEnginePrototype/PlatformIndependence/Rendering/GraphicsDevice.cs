namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering
{
    public abstract class GraphicsDevice
    {
        public abstract void Clear(bool color = true, bool depth = true);
        public abstract void Clear(float r = 0, float g = 0, float b = 0, float a = 1, bool color = true, bool depth = true);
        public abstract void SetViewport(int width, int height);
        public abstract void Dispose();
    }
}
