using TerraWorldEnginePrototype.Core.Mathematics;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering
{
    public abstract class GraphicsDevice
    {
        public abstract void Clear();
        public abstract void Clear(float r, float g, float b, float a);
        public abstract void Clear(Color color);
        public abstract void SetViewport(int width, int height);
    }
}
