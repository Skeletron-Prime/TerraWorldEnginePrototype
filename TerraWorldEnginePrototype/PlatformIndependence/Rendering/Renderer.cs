using System.Numerics;
using TerraWorldEnginePrototype.Core.Mathematics;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering
{
    public abstract class Renderer
    {
        public abstract void Draw(Mesh mesh);

        public abstract void Model(ref Matrix4x4 model);
        public abstract void View(ref Matrix4x4 view);
        public abstract void Projection(ref Matrix4x4 projection);
    }
}
