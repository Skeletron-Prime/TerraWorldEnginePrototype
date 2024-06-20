namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives
{
    public enum ReadBufferMode : uint
    {
        None = 0,
        FrontLeft = 0x0400,
        FrontRight = 0x0401,
        BackLeft = 0x0402,
        BackRight = 0x0403,
        Front = 0x0404,
        Back = 0x0405,
        Left = 0x0406,
        Right = 0x0407,
        FrontAndBack = 0x0408,
        Aux0 = 0x0409,
        Aux1 = 0x040A,
        Aux2 = 0x040B,
        Aux3 = 0x040C,
    }
}