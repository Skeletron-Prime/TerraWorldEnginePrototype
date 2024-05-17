using System.Numerics;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager
{
    public class WindowSettings
    {
        public string Title { get; set; } = "TerraWorld Engine";

        public Vector2 Size { get; set; } = new Vector2(800, 600);

        public Vector2 Location { get; set; } = new Vector2(1200, 400);

        public bool IsVisible { get; set; } = true;
    }
}
