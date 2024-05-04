﻿namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.Primitives
{
    internal enum ClearBufferMask : uint
    {
        DepthBufferBit = 0x00000100,
        StencilBufferBit = 0x00000400,
        ColorBufferBit = 0x00004000,
    }
}