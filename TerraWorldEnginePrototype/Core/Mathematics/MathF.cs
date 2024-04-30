namespace TerraWorldEnginePrototype.Core.Mathematics
{
    /// <summary>
    /// Provides constants and static methods for trigonometric, logarithmic, and other common mathematical functions.
    /// </summary>
    public struct MathF
    {
        public static float Abs(float value)
        {
            return value >= 0 ? value : -value;
        }

        public static float Sqrt(float value)
        {
            return (float)Math.Sqrt(value);
        }

        public static float Pow(float x, float y)
        {
            return (float)Math.Pow(x, y);
        }

        public static float Sin(float value)
        {
            return (float)Math.Sin(value);
        }

        public static float Cos(float value)
        {
            return (float)Math.Cos(value);
        }

        public static float Tan(float value)
        {
            return (float)Math.Tan(value);
        }

        public static float Asin(float value)
        {
            return (float)Math.Asin(value);
        }

        public static float Acos(float value)
        {
            return (float)Math.Acos(value);
        }

        public static float Atan(float value)
        {
            return (float)Math.Atan(value);
        }

        public static float Atan2(float y, float x)
        {
            return (float)Math.Atan2(y, x);
        }

        public static float Log(float value)
        {
            return (float)Math.Log(value);
        }

        public static float Log(float value, float newBase)
        {
            return (float)Math.Log(value, newBase);
        }

        public static float Log10(float value)
        {
            return (float)Math.Log10(value);
        }

        public static float Exp(float value)
        {
            return (float)Math.Exp(value);
        }

        public static float Floor(float value)
        {
            return (float)Math.Floor(value);
        }

        public static float Ceiling(float value)
        {
            return (float)Math.Ceiling(value);
        }

        public static float Round(float value)
        {
            return (float)Math.Round(value);
        }

        public static float Round(float value, int digits)
        {
            return (float)Math.Round(value, digits);
        }

        public static float Truncate(float value)
        {
            return (float)Math.Truncate(value);
        }

        public static float Min(float value1, float value2)
        {
            return value1 < value2 ? value1 : value2;
        }

        public static float Max(float value1, float value2)
        {
            return value1 > value2 ? value1 : value2;
        }

        public static float DegreesToRadians(float degrees)
        {
            return degrees * (float)Math.PI / 180.0f;
        }

        public static float RadiansToDegrees(float radians)
        {
            return radians * 180.0f / (float)Math.PI;
        }

        public static float Clamp(float value, float min, float max)
        {
            if (min > max)
            {
                throw new ArgumentException("Min value is greater than Max value!");
            }

            return value < min ? min : value > max ? max : value;
        }
    }
}
