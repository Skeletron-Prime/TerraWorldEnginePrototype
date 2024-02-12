using System.Diagnostics.CodeAnalysis;

namespace CoreEnginePrototype
{
    /// <summary>
    /// Quaternions are used to represent rotations in 3D space. They are more stable than Euler angles, and can be used to avoid gimbal lock.
    /// </summary>
    public struct Quaternion
    {
        /// <summary>
        /// The w component of the quaternion. Represents the scalar part of the quaternion.
        /// </summary>
        public float w;

        /// <summary>
        /// The x component of the quaternion. Represents the rotation around the x axis.
        /// </summary>
        public float x;

        /// <summary>
        /// The y component of the quaternion. Represents the rotation around the y axis.
        /// </summary>
        public float y;

        /// <summary>
        /// The z component of the quaternion. Represents the rotation around the z axis.
        /// </summary>
        public float z;

        /// <summary>
        /// Returns the identity quaternion. The identity quaternion is a quaternion with 0 rotation.
        /// </summary>
        public static Quaternion Identity => new Quaternion(1, 0, 0, 0);

        /// <summary>
        /// Returns the normalized quaternion. The normalized quaternion is a quaternion with the same rotation as the original quaternion, but with a magnitude of 1.
        /// </summary>
        public Quaternion Normalized
        {
            get => Internal_Normalize();
        }

        /// <summary>
        /// Returns the vector parameter w, x, y and z with relevant indexes 0, 1, 2 and 3, otherwise throws an IndexOutOfRangeException.
        /// </summary>
        /// <param name="index">The index of the Vector, if 0 it is w, 1 it is x, 2 it is y, 3 it is z, otherwise exception.</param>
        /// <returns>The vector parameter w, x, y and z with relevand indexes 0, 1, 2 and 3, otherwise throws an IndexOutOfRangeException.</returns>
        /// <exception cref="IndexOutOfRangeException">If index was not 0 or 1 or 2 or 3, the index is out of the range.</exception>
        public float this[int index]
        {
            readonly get
            {
                return index switch
                {
                    0 => w,
                    1 => x,
                    2 => y,
                    3 => z,
                    _ => throw new IndexOutOfRangeException("Index was out of Quaternion range!"),
                };
            }

            set
            {
                switch (index)
                {
                    case 0:
                        w = value;
                        break;
                    case 1:
                        x = value;
                        break;
                    case 2:
                        y = value;
                        break;
                    case 3:
                        z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Index was out of Quaternion range!");
                }
            }
        }

        /// <summary>
        /// Creates a new quaternion using the given parameter w and the vector part of the quaternion.
        /// </summary>
        /// <param name="w">Sets the scalar part of the quaternion.</param>
        /// <param name="vectorPart">Sets the given coordinates x, y and z.</param>
        public Quaternion(float w, Vector3 vectorPart)
        {
            this.w = w;
            x = vectorPart.x;
            y = vectorPart.y;
            z = vectorPart.z;
        }

        /// <summary>
        /// Creates a new quaternion using the given parameters w, x, y and z.
        /// </summary>
        /// <param name="w">Sets the scalar part of the quaternion.</param>
        /// <param name="x">Sets the x coordinate of the quaternion.</param>
        /// <param name="y">Sets the y coordinate of the quaternion.</param>
        /// <param name="z">Sets the z coordinate of the quaternion.</param>
        public Quaternion(float w, float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Compares this quaternion with the given object for equality. Returns true if the object is a quaternion and is equal to this quaternion, otherwise returns false.
        /// </summary>
        /// <param name="obj">Any object in c#. The object to be compared with.</param>
        /// <returns>True if the given object is equal to this quaternion, otherwise false.</returns>
        public readonly override bool Equals([NotNullWhen(true)]object? obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Compares this quaternion with the given quaternion for equality. Returns true if the given quaternions parameters are equal to this quaternion, otherwise returns false.
        /// </summary>
        /// <param name="other">Other quaternion to be compared with.</param>
        /// <returns>True if the parameters are equal to the parameters of this quaternion, otherwise false.</returns>
        public readonly bool Equals(Quaternion other)
        {
            return w == other.w && x == other.x && y == other.y && z == other.z;
        }

        /// <summary>
        /// Compares two quaternions for equality. Returns true if the parameters of the given quaternions are equal, otherwise returns false.
        /// </summary>
        /// <param name="a">First quaternion, that should be compared with the second one, by the parameters.</param>
        /// <param name="b">Second quaternion, with which will fist quaternion compared.</param>
        /// <returns>True if the parameters of both quaternions are equal, otherwise false.</returns>
        public static bool Equals(Quaternion a, Quaternion b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Returns the hash code for this quaternion.
        /// </summary>
        /// <returns>The hash code for this quaternion.</returns>
        public readonly override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Returns the string representation of the quaternion.
        /// </summary>
        /// <returns>The string representation of the quaternion, as w, x, y and z string.</returns>
        public readonly override string ToString()
        {
            return $"({w}, {x}, {y}, {z})";
        }

        /// <summary>
        /// Normalizes the quaternion. The normalized quaternion is a quaternion with the same rotation as the original quaternion, but with a magnitude of 1.
        /// </summary>
        /// <returns>The normalized quaternion.</returns>
        private readonly Quaternion Internal_Normalize()
        {
            float magnitude = Internal_Magnitude();
            return new Quaternion(w / magnitude, x / magnitude, y / magnitude, z / magnitude);
        }

        /// <summary>
        /// Returns the conjugate of the quaternion. The conjugate of the quaternion is a quaternion with the same rotation as the original quaternion, but with the opposite direction.
        /// </summary>
        /// <returns>The conjugate of the quaternion.</returns>
        private readonly Quaternion Internal_Conjugate()
        {
            return new Quaternion(w, -x, -y, -z);
        }

        /// <summary>
        /// Returns the magnitude of the quaternion. The magnitude of the quaternion is the square root of the sum of the squares of the components.
        /// </summary>
        /// <returns>The magnitude of the quaternion.</returns>
        private readonly float Internal_Magnitude()
        {
            return MathF.Sqrt(w * w + x * x + y * y + z * z);
        }

        /// <summary>
        /// Returns the magnitude of the quaternion squared. The magnitude of the quaternion is the sum of the squares of the components.
        /// </summary>
        /// <returns>The magnitude of the quaternion squared.</returns>
        private readonly float Internal_MagnitudeSquared()
        {
            return w * w + x * x + y * y + z * z;
        }

        /// <summary>
        /// Normalizes the quaternion. The normalized quaternion is a quaternion with the same rotation as the original quaternion, but with a magnitude of 1.
        /// </summary>
        /// <returns>The normalized quaternion.</returns>
        public readonly Quaternion Normalize()
        {
            return Internal_Normalize();
        }

        /// <summary>
        /// Returns the magnitude of the quaternion. The magnitude of the quaternion is the square root of the sum of the squares of the components.
        /// </summary>
        /// <returns>The magnitude of the quaternion.</returns>
        public readonly float Magnitude()
        {
            return Internal_Magnitude();
        }

        /// <summary>
        /// Returns the magnitude of the quaternion squared. The magnitude of the quaternion is the sum of the squares of the components.
        /// </summary>
        /// <returns>The magnitude of the quaternion squared.</returns>
        public readonly float MagnitudeSquared()
        {
            return Internal_MagnitudeSquared();
        }

        /// <summary>
        /// Returns the conjugate of the quaternion. The conjugate of the quaternion is a quaternion with the same rotation as the original quaternion, but with the opposite direction.
        /// </summary>
        /// <returns>The conjugate of the quaternion.</returns>
        public readonly Quaternion Conjugate()
        {
            return Internal_Conjugate();
        }

        /// <summary>
        /// Normalizes the quaternion. The normalized quaternion is a quaternion with the same rotation as the original quaternion, but with a magnitude of 1.
        /// </summary>
        /// <param name="quaternion">The quaternion that must be normalized.</param>
        /// <returns>The normalized quaternion.</returns>
        public static Quaternion Normalize(Quaternion quaternion)
        {
            return quaternion.Internal_Normalize();
        }

        /// <summary>
        /// Returns the magnitude of the quaternion. The magnitude of the quaternion is the square root of the sum of the squares of the components.
        /// </summary>
        /// <param name="quaternion">The quaternion, to return the magnitude of.</param>
        /// <returns>The magnitude of the quaternion.</returns>
        public static float Magnitude(Quaternion quaternion)
        {
            return quaternion.Internal_Magnitude();
        }


        /// <summary>
        /// Returns the magnitude of the quaternion squared. The magnitude of the quaternion is the sum of the squares of the components.
        /// </summary>
        /// <param name="quaternion">The quaternion, to return the squared magnitude of.</param>
        /// <returns>The magnitude of the quaternion squared.</returns>
        public static float MagnitudeSquared(Quaternion quaternion)
        {
            return quaternion.Internal_MagnitudeSquared();
        }

        /// <summary>
        /// Returns the conjugate of the quaternion. The conjugate of the quaternion is a quaternion with the same rotation as the original quaternion, but with the opposite direction.
        /// </summary>
        /// <param name="quaternion">The quaternion, to return the conjugate of.</param>
        /// <returns>The conjugate of the quaternion.</returns>
        public static Quaternion Conjugate(Quaternion quaternion)
        {
            return quaternion.Internal_Conjugate();
        }

        /// <summary>
        /// Returns the product of the given quaternions.
        /// </summary>
        /// <param name="a">Multiplicand quaternion value.</param>
        /// <param name="b">Multiplier quaternion value.</param>
        /// <returns>The product of the given quaternions.</returns>
        public static Quaternion operator *(Quaternion a, Quaternion b)
        {
            return new Quaternion(
                a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z, 
                a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y, 
                a.w * b.y - a.x * b.z + a.y * b.w + a.z * b.x, 
                a.w * b.z + a.x * b.y - a.y * b.x + a.z * b.w
                );
        }

        /// <summary>
        /// Returns the division of the given quaternions.
        /// </summary>
        /// <param name="a">Dividend quaternion value.</param>
        /// <param name="b">Divisor quaternion value.</param>
        /// <returns>The division of the given quaternions.</returns>
        public static Quaternion operator /(Quaternion a, Quaternion b)
        {
            return new Quaternion(
                (a.w * b.w + a.x * b.x + a.y * b.y + a.z * b.z) / b.Internal_MagnitudeSquared(),
                (a.x * b.w - a.w * b.x - a.y * b.z + a.z * b.y) / b.Internal_MagnitudeSquared(),
                (a.y * b.w + a.w * b.y - a.x * b.z - a.z * b.x) / b.Internal_MagnitudeSquared(),
                (a.z * b.w + a.w * b.z + a.x * b.y - a.y * b.x) / b.Internal_MagnitudeSquared()
                );
        }
    }
}
