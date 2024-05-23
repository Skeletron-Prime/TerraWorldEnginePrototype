using System.Numerics;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.WindowManager
{
    public readonly struct ResizeEventArgs
    {
        public Vector2 Size { get; }

        public int Width => (int)Size.X;

        public int Height => (int)Size.Y;

        public ResizeEventArgs(Vector2 size)
        {
            Size = size;
        }

        public ResizeEventArgs(int width, int height) : this(new Vector2(width, height))
        {
        }
    }
}
