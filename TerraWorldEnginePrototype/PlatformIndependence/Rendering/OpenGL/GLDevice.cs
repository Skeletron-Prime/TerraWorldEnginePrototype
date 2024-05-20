using TerraWorldEnginePrototype.Core.Mathematics;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    public class GLDevice : GraphicsDevice
    {
        public override void Clear()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public override void Clear(float r, float g, float b, float a)
        {
            GL.ClearColor(r, g, b, a);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public override void Clear(Color color)
        {
            GL.ClearColor(color.R, color.G, color.B, color.A);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public override void SetViewport(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
        }
    }
}
