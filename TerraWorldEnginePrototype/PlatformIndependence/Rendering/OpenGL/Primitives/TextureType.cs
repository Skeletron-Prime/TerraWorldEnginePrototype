namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives
{
    public enum TextureType : uint
    {
        Texture1D = 0x0DE0,
        Texture2D = 0x0DE1,
        Texture3D = 0x806F,
        TextureCubeMap = 0x8513,
        Texture1DArray = 0x8C18,
        Texture2DArray = 0x8C1A,
        TextureCubeMapArray = 0x9009,
        TextureRectangle = 0x84F5,
        TextureBuffer = 0x8C2A,
        Texture2DMultisample = 0x9100,
        Texture2DMultisampleArray = 0x9102
    }
}