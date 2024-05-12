namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives
{
    public enum ClearBufferMask : uint
    {
        ColorBufferBit = 0x00004000,
        DepthBufferBit = 0x00000100,
        StencilBufferBit = 0x00000400
    }
}