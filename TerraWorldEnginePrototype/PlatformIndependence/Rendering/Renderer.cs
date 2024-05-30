using System.Numerics;
using System.Runtime.InteropServices;
using TerraWorldEnginePrototype.Core.Mathematics;
using TerraWorldEnginePrototype.Graphics;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering
{
    public abstract class Renderer
    {
        public static Renderer Create()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new OpenGL.GLRenderer();
            }
            else
            {
                throw new PlatformNotSupportedException("Windows only!");
            }
        }

        public abstract void Draw(EngineObject mesh);

        public abstract void Model(Matrix4x4 model);
        public abstract void View(Matrix4x4 view);
        public abstract void Projection(Matrix4x4 projection);
    }
}
