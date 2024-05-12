namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    public interface IGLObject
    {
        uint Id { get; }
        IGLObjectType Type { get; }
    }
}