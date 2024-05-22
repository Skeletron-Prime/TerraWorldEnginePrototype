namespace TerraWorldEnginePrototype.Core.Mathematics
{
    public struct Color
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public static Color White => new Color(1.0f, 1.0f, 1.0f, 1.0f);
        public static Color Black => new Color(0.0f, 0.0f, 0.0f, 1.0f);
        public static Color Red => new Color(1.0f, 0.0f, 0.0f, 1.0f);
        public static Color Green => new Color(0.0f, 1.0f, 0.0f, 1.0f);
        public static Color Blue => new Color(0.0f, 0.0f, 1.0f, 1.0f);
        public static Color Yellow => new Color(1.0f, 1.0f, 0.0f, 1.0f);
        public static Color Purple => new Color(1.0f, 0.0f, 1.0f, 1.0f);
        public static Color Cyan => new Color(0.0f, 1.0f, 1.0f, 1.0f);

        public Color(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }
}
