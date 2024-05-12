namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives
{
    public enum TextureParameterName : uint
    {
        TextureWrapS = 0x2802,
        TextureWrapT = 0x2803,
        TextureWrapR = 0x8072,
        TextureMinFilter = 0x2801,
        TextureMagFilter = 0x2800,
        TextureMaxAnisotropy = 0x84FE,
        TextureBaseLevel = 0x813C,
        TextureMaxLevel = 0x813D,
        TextureCompareMode = 0x884C,
        TextureCompareFunc = 0x884D,
        TextureBorderColor = 0x1004,
        TextureSwizzleR = 0x8E42,
        TextureSwizzleG = 0x8E43,
        TextureSwizzleB = 0x8E44,
        TextureSwizzleA = 0x8E45,
        TextureSwizzleRGBA = 0x8E46
    }
}