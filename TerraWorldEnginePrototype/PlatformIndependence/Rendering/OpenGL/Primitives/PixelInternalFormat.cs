namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives
{
    public enum PixelInternalFormat : uint
    {
        Rgba = 0x1908,
        Rgb = 0x1907,
        Rgba8 = 0x8058,
        Rgb8 = 0x8051,
        Rgba16f = 0x881A,
        Rgb16f = 0x881B,
        Rgba32f = 0x8814,
        Rgb32f = 0x8815,
        DepthComponent = 0x1902,
        DepthComponent16 = 0x81A5,
        DepthComponent24 = 0x81A6,
        DepthComponent32 = 0x81A7,
        Depth24Stencil8 = 0x88F0,
        Depth32fStencil8 = 0x8CAD
    }
}