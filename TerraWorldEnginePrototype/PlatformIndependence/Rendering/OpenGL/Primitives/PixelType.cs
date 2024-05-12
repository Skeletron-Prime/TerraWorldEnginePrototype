namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives
{
    public enum PixelType : uint
    {
        UnsignedByte = 0x1401,
        Byte = 0x1400,
        UnsignedShort = 0x1403,
        Short = 0x1402,
        UnsignedInt = 0x1405,
        Int = 0x1404,
        Float = 0x1406,
        HalfFloat = 0x140B,
        UnsignedShort4444 = 0x8033,
        UnsignedShort5551 = 0x8034,
        UnsignedShort565 = 0x8363,
        UnsignedInt8888 = 0x8035,
        UnsignedInt1010102 = 0x8036,
        UnsignedInt248 = 0x84FA
    }
}