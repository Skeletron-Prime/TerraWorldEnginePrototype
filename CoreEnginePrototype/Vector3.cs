using System.Diagnostics.CodeAnalysis;

namespace CoreEnginePrototype
{
    /// <summary>
    /// Represents a 3D vector with x, y and z components, to represent the location in 3D space.
    /// </summary>
    public struct Vector3
    {
        /// <summary>
        /// The x component of the vector. In right-handed coordinate system, this is the right direction.
        /// </summary>
        public float x;

        /// <summary>
        /// The y component of the vector. In right-handed coordinate system, this is the up direction.
        /// </summary>
        public float y;

        /// <summary>
        /// The z component of the vector. In right-handed coordinate system, this is the forward direction.
        /// </summary>
        public float z;

        /// <summary>
        /// Returns a vector with all its components set to zero.
        /// </summary>
        public static Vector3 Zero => new(0, 0, 0);

        /// <summary>
        /// Returns a vector with all its components set to one.
        /// </summary>
        public static Vector3 One => new(1, 1, 1);

        /// <summary>
        /// Returns a vector with z component set to one and all other components set to zero.
        /// </summary>
        public static Vector3 Forward => new(0, 0, 1);

        /// <summary>
        /// Returns a vector with z component set to -1 and all other components set to zero.
        /// </summary>
        public static Vector3 Backward => new(0, 0, -1);

        /// <summary>
        /// Returns a vector with y component set to one and all other components set to zero.
        /// </summary>
        public static Vector3 Up => new(0, 1, 0);

        /// <summary>
        /// Returns a vector with y component set to -1 and all other components set to zero.
        /// </summary>
        public static Vector3 Down => new(0, -1, 0);

        /// <summary>
        /// Returns a vector with x component set to -1 and all other components set to zero.
        /// </summary>
        public static Vector3 Left => new(-1, 0, 0);

        /// <summary>
        /// Returns a vector with x component set to one and all other components set to zero.
        /// </summary>
        public static Vector3 Right => new(1, 0, 0);

        /// <summary>
        /// Returns a vector with all of its components set to positive infinity.
        /// </summary>
        public static Vector3 Infinity => new(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

        /// <summary>
        /// Returns a vector with all of its components set to negative infinity.
        /// </summary>
        public static Vector3 NegativeInfinity => new(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

        /// <summary>
        /// Returns this normalized vector.
        /// </summary>
        public Vector3 Normalized
        {
            get => Internal_Normalize();
        }

        /// <summary>
        /// Returns the vector parameter x, y and z with relevant indexes 0, 1 and 2, otherwise throws an IndexOutOfRangeException.
        /// </summary>
        /// <param name="index">The index of the Vector, if 0 it is x, 1 it is y, 2 it is z, otherwise exception.</param>
        /// <returns>The vector parameter x, y and z with relevand indexes 0, 1 and 2, otherwise throws an IndexOutOfRangeException.</returns>
        /// <exception cref="IndexOutOfRangeException">If index was not 0 or 1 or 2, the index is out of the range.</exception>
        public float this[int index]
        {
            readonly get => index switch
            {
                0 => x,
                1 => y,
                2 => z,
                _ => throw new IndexOutOfRangeException("Index was out of Vector3 range")
            };

            set
            {
                switch (index)
                {
                    case 0:
                        x = value;
                        break;
                    case 1:
                        y = value;
                        break;
                    case 2:
                        z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Index was out of Vector3 range");
                }
            }
        }

        /// <summary>
        /// Creates a new vector with all its components set to zero.
        /// </summary>
        public Vector3()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        /// <summary>
        /// Creates a new vector with given x and y components and sets the z component to zero.
        /// </summary>
        /// <param name="x">Sets the x coordinate</param>
        /// <param name="y">Sets the y coordinate</param>
        public Vector3(float x, float y)
        {
            this.x = x;
            this.y = y;
            z = 0;
        }

        /// <summary>
        /// Creates a new vector with all its components set to the given value.
        /// </summary>
        /// <param name="x">Sets the x coordinate.</param>
        /// <param name="y">Sets the y coordinate.</param>
        /// <param name="z">Sets the z coordinate.</param>
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Uses the base Equals method to compare the given object with this object.
        /// </summary>
        /// <param name="obj">The object to be compared with.</param>
        /// <returns>True if this object is comparable to the given one, otherwise false.</returns>
        public override readonly bool Equals([NotNullWhen(true)] object? obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Compares the given vector with this vector.
        /// </summary>
        /// <param name="other">The vector to be compared with.</param>
        /// <returns>True if the values of compared vectors are the same, otherwise false.</returns>
        public readonly bool Equals(Vector3 other)
        {
            return x == other.x && y == other.y && z == other.z;
        }

        /// <summary>
        /// Compares the given vectors.
        /// </summary>
        /// <param name="a">First vector, which must be compared with the second one.</param>
        /// <param name="b">Second vector, that will be compared with the first one.</param>
        /// <returns>True if both vectors are equal.</returns>
        public static bool Equals(Vector3 a, Vector3 b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Returns the hash code of this object.
        /// </summary>
        /// <returns>The hash code of this object</returns>
        public override readonly int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>String out of the values "x", "y" and "z".</returns>
        public override readonly string ToString()
        {
            return $"({x}, {y}, {z})";
        }

        /// <summary>
        /// Returns a string representation of the given vector.
        /// </summary>
        /// <param name="vector">The vector, out which the string will be built.</param>
        /// <returns>A string representation of the given vector.</returns>
        public static string ToString(Vector3 vector)
        {
            return vector.ToString();
        }

        /// <summary>
        /// Returns the magnitude of the vector.
        /// </summary>
        /// <returns>The magnitude of the vector.</returns>
        private readonly float Internal_Magnitude()
        {
            return MathF.Sqrt(x * x + y * y + z * z);
        }

        /// <summary>
        /// Returns the squared magnitude of the vector.
        /// </summary>
        /// <returns>The squared magnitude of the vector.</returns>
        private readonly float Internal_MagnitudeSquared()
        {
            return x * x + y * y + z * z;
        }

        /// <summary>
        /// Returns the cross product of the given vectors.
        /// </summary>
        /// <param name="other">Second vector of the cross.</param>
        /// <returns>The cross product of the given vectors.</returns>
        private readonly Vector3 Internal_Cross(Vector3 other)
        {
            return new Vector3(y * other.z - z * other.y, z * other.x - x * other.z, x * other.y - y * other.x);
        }

        /// <summary>
        /// Normalizes the vector. The vector will have the same direction, but its magnitude will be 1. 
        /// </summary>
        /// <returns>The normalized vector.</returns>
        private readonly Vector3 Internal_Normalize()
        {
            float magnitude = Internal_Magnitude();
            return new Vector3(x / magnitude, y / magnitude, z / magnitude);
        }

        /// <summary>
        /// Returns the dot product of the given vectors.
        /// </summary>
        /// <param name="other">Second vector, that parameters multiplie the parameters of this vector.</param>
        /// <returns>The dot product of the given vectors.</returns>
        private readonly float Internal_Dot(Vector3 other)
        {
            return x * other.x + y * other.y + z * other.z;
        }

        /// <summary>
        /// Returns the distance between the given vectors. Float value, that represents the distance between the given vectors.
        /// </summary>
        /// <param name="other">Another vector of the lerp.</param>
        /// <param name="t">Is a value between 0 and 1, where 0 is the positon of vector a and 1 is positon of vector b, so it represents the distance between the given vectors.</param>
        /// <returns>The distance between the given vectors.</returns>
        private readonly Vector3 Internal_Lerp(Vector3 other, float t)
        {
            t = MathF.Clamp(t, 0, 1);

            return new Vector3(x + (other.x - x) * t, y + (other.y - y) * t, z + (other.z - z) * t);
        }

        /// <summary>
        /// Returns the angle between the given vectors.
        /// </summary>
        /// <param name="other">Second vector, to this vector the angel will be calculated.</param>
        /// <returns>The angle between the given vectors.</returns>
        private readonly float Internal_Angle(Vector3 other)
        {
            float magnitude = Internal_Magnitude() * other.Internal_Magnitude();

            if (magnitude < 1e-15f)
            {
                return 0;
            }

            float dot = MathF.Clamp(Internal_Dot(other) / magnitude, -1, 1);

            return MathF.Acos(dot) * 57.29578f;
        }

        /// <summary>
        /// Returns the magnitude of the vector.
        /// </summary>
        /// <returns>The magnitude of the vector.</returns>
        public readonly float Magnitude()
        {
            return Internal_Magnitude();
        }

        /// <summary>
        /// Returns the squared magnitude of the vector.
        /// </summary>
        /// <returns>The squared magnitude of the vector.</returns>
        public readonly float MagnitudeSquared()
        {
            return Internal_MagnitudeSquared();
        }

        /// <summary>
        /// Returns the cross product of the given vectors.
        /// </summary>
        /// <param name="other">Second vector of the cross.</param>
        /// <returns>The cross product of the given vectors.</returns>
        public readonly Vector3 Cross(Vector3 other)
        {
            return Internal_Cross(other);
        }

        /// <summary>
        /// Normalizes the vector. The vector will have the same direction, but its magnitude will be 1. 
        /// </summary>
        /// <returns>The normalized vector.</returns>
        public readonly Vector3 Normalize()
        {
            return Internal_Normalize();
        }

        /// <summary>
        /// Returns the dot product of the given vectors.
        /// </summary>
        /// <param name="other">Second vector, that parameters multiplie the parameters of this vector.</param>
        /// <returns>The dot product of the given vectors.</returns>
        public readonly float Dot(Vector3 other)
        {
            return Internal_Dot(other);
        }

        /// <summary>
        /// Returns the distance between the given vectors. Float value, that represents the distance between the given vectors.
        /// </summary>
        /// <param name="other">Another vector of the lerp.</param>
        /// <param name="t">Is a value between 0 and 1, where 0 is the positon of vector a and 1 is positon of vector b, so it represents the distance between the given vectors.</param>
        /// <returns>The distance between the given vectors.</returns>
        public readonly Vector3 Lerp(Vector3 other, float t)
        {
            return Internal_Lerp(other, t);
        }

        /// <summary>
        /// Returns the angle between the given vectors.
        /// </summary>
        /// <param name="other">Second vector, to this vector the angel will be calculated.</param>
        /// <returns>The angle between the given vectors.</returns>
        public readonly float Angle(Vector3 other)
        {
            return Internal_Angle(other);
        }

        /// <summary>
        /// Returns the magnitude of the vector.
        /// </summary>
        /// <param name="vector">The vector, to return the magnitude of.</param>
        /// <returns>The magnitude of the vector.</returns>
        public static float Magnitude(Vector3 vector)
        {
            return vector.Internal_Magnitude();
        }

        /// <summary>
        /// Returns the squared magnitude of the vector.
        /// </summary>
        /// <param name="vector">The vector, to return the squared magnitude of.</param>
        /// <returns>The squared magnitude of the vector.</returns>
        public static float MagnitudeSquared(Vector3 vector)
        {
            return vector.Internal_MagnitudeSquared();
        }

        /// <summary>
        /// Returns the cross product of the given vectors.
        /// </summary>
        /// <param name="a">First vector of the cross.</param>
        /// <param name="b">Second vector of the cross.</param>
        /// <returns>The cross product of the given vectors.</returns>
        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            return a.Internal_Cross(b);
        }

        /// <summary>
        /// Normalizes the vector. The vector will have the same direction, but its magnitude will be 1. 
        /// </summary>
        /// <param name="vector">The vector, that must be normalized</param>
        /// <returns>The normalized vector.</returns>
        public static Vector3 Normalize(Vector3 vector)
        {
            return vector.Internal_Normalize();
        }

        /// <summary>
        /// Returns the dot product of the given vectors.
        /// </summary>
        /// <param name="a">First vector, which parameters will be multiplied with the second one to produce dot product.</param>
        /// <param name="b">Second vector, that parameters multiplie the parameters of the first one.</param>
        /// <returns>The dot product of the given vectors.</returns>
        public static float Dot(Vector3 a, Vector3 b)
        {
            return a.Internal_Dot(b);
        }

        /// <summary>
        /// Returns the distance between the given vectors. Float value, that represents the distance between the given vectors.
        /// </summary>
        /// <param name="a">First position.</param>
        /// <param name="b">Second position.</param>
        /// <param name="t">Is a value between 0 and 1, where 0 is the positon of vector a and 1 is positon of vector b, so it represents the distance between the given vectors.</param>
        /// <returns>The distance between the given vectors.</returns>
        public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
        {
            return a.Internal_Lerp(b, t);
        }

        /// <summary>
        /// Returns the angle between the given vectors.
        /// </summary>
        /// <param name="from">First vector, from which the angel will be calculated.</param>
        /// <param name="to">Second vector, to this vector the angel will be calculated.</param>
        /// <returns>The angle between the given vectors.</returns>
        public static float Angle(Vector3 from, Vector3 to)
        {
            return from.Internal_Angle(to);
        }

        /// <summary>
        /// Returns the dot product of the given vectors.
        /// </summary>
        /// <param name="a">Augend vector value.</param>
        /// <param name="b">Addend vector value.</param>
        /// <returns>The dot product of the given vectors.</returns>
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        /// <summary>
        /// Returns the sum of the given vector and the given float.
        /// </summary>
        /// <param name="a">Augend vector value.</param>
        /// <param name="b">Addend flaot value, that will be added to every parameter of vector a.</param>
        /// <returns>The sum of the given vector and the given float.</returns>
        public static Vector3 operator +(Vector3 a, float b)
        {
            return new Vector3(a.x + b, a.y + b, a.z + b);
        }

        /// <summary>
        /// Returns the difference of the given vectors.
        /// </summary>
        /// <param name="a">Minuend vector value.</param>
        /// <param name="b">Subtrahend vector value.</param>
        /// <returns>The difference of the given vectors.</returns>
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        /// <summary>
        /// Returns the difference of the given vector and the given float.
        /// </summary>
        /// <param name="a">Minuend vector value.</param>
        /// <param name="b">Subtrahend float velue, that will be subtract from every value of vector a.</param>
        /// <returns>The difference of the given vector and the given flaot.</returns>
        public static Vector3 operator -(Vector3 a, float b)
        {
            return new Vector3(a.x - b, a.y - b, a.z - b);
        }

        /// <summary>
        /// Returns the product of the given vectors.
        /// </summary>
        /// <param name="a">Multiplicand vector value.</param>
        /// <param name="b">Multiplier vector value.</param>
        /// <returns>The product of the given vectors.</returns>
        public static Vector3 operator *(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        /// <summary>
        /// Returns the product of the given vector and the given float.
        /// </summary>
        /// <param name="a">Multiplicand vector value.</param>
        /// <param name="b">Multiplier float value, on which every vector value will be multiplied</param>
        /// <returns></returns>
        public static Vector3 operator *(Vector3 a, float b)
        {
            return new Vector3(a.x * b, a.y * b, a.z * b);
        }

        /// <summary>
        /// Returns the division of the given vectors.
        /// </summary>
        /// <param name="a">Dividend vector value.</param>
        /// <param name="b">Divisor vector value.</param>
        /// <returns>The division of the given vectors.</returns>
        public static Vector3 operator /(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
        }

        /// <summary>
        /// Returns the division of the given vector and the given float.
        /// </summary>
        /// <param name="a">Dividend vector value.</param>
        /// <param name="b">Divisor float value, on which every value in vector will be divided.</param>
        /// <returns>The division of the given vector and the given float.</returns>
        public static Vector3 operator /(Vector3 a, float b)
        {
            return new Vector3(a.x / b, a.y / b, a.z / b);
        }

        /// <summary>
        /// Compares the two given object and returns true if they are equal, otherwise false.
        /// </summary>
        /// <param name="left">First object, which will be compared with the second one.</param>
        /// <param name="right">Second object, that will be compared with the first one.</param>
        /// <returns>True if the object are equal, otherwise false.</returns>
        public static bool operator ==(Vector3 left, Vector3 right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares the two given object and returns true if they are not equal, otherwise false.
        /// </summary>
        /// <param name="left">First object, which will be compared with the second one.</param>
        /// <param name="right">Second object, that will be compared with the first one.</param>
        /// <returns>True if the given object are not equal, otherwise false.</returns>
        public static bool operator !=(Vector3 left, Vector3 right)
        {
            return !(left == right);
        }
    }
}
