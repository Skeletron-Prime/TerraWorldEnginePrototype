using System.Numerics;
using System.Runtime.InteropServices;
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

        public abstract void DrawScene(Scene scene);

        protected abstract void Model(Matrix4x4 model);
        protected abstract void View(Matrix4x4 view);
        protected abstract void Projection(Matrix4x4 projection);
    }
}
