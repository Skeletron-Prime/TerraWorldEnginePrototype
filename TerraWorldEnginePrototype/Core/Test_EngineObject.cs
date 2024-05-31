using System.Numerics;
using TerraWorldEnginePrototype.Core.Mathematics;
using TerraWorldEnginePrototype.Graphics;

namespace TerraWorldEnginePrototype.Core
{
    public class Test_EngineObject
    {
        /// <summary>
        /// The next ID that will be assigned to the next object.
        /// </summary>
        static int NextID = 1;

        /// <summary>
        /// The ID of the object.
        /// </summary>
        public int ID { get; } = NextID++;

        /// <summary>
        /// The name of the object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the object.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Create a new instance of the class Object.
        /// </summary>
        public Test_EngineObject() : this(null, null) { }

        /// <summary>
        /// Create a new instance of the class Object with the given name and description.
        /// </summary>
        /// <param name="name">The name of created object.</param>
        /// <param name="description">The description of created object.</param>
        public Test_EngineObject(string? name, string? description)
        {
            Name = name ?? "New" + GetType().Name;
            Description = description ?? "New" + GetType().Name + "object";
        }

        /// <summary>
        /// If the object equals to this object return true, otherwise return false.
        /// </summary>
        /// <param name="obj">Any instance of the class Object.</param>
        /// <returns>True if the given object is equals to this object.</returns>
        public bool Equals(Test_EngineObject? obj)
        {
            // If the given object is null return false.
            if (obj == null)
            {
                return false;
            }

            // Compare them and return the result.
            return this == obj;
        }

        /// <summary>
        /// If the object A equals to the object B return true, otherwise return false.
        /// </summary>
        /// <param name="objA">Any instance of the class Object.</param>
        /// <param name="objB">Any instance of the class Object.</param>
        /// <returns>True if object A equals to the object B.</returns>
        public static bool Equals(Test_EngineObject? objA, Test_EngineObject? objB)
        {
            // If any of the given objects is null return false.
            if (objA == null || objB == null)
            {
                return false;
            }

            // Compare them and return the result.
            return objA == objB;
        }

        /// <summary>
        /// If the object is null throw a NullReferenceException.
        /// </summary>
        /// <param name="obj">Any object that exists in C#, that must not be null.</param>
        public static void IsInstanceNull(object? obj)
        {
            Internal_IsInstanceNull(obj);
        }

        /// <summary>
        /// If the object is null throw a NullReferenceException with the given name.
        /// </summary>
        /// <param name="obj">Any object that exists in C#, that must not be null.</param>
        /// <param name="name">The name of the given object.</param>
        public static void IsInstanceNull(object? obj, string name)
        {
            Internal_IsInstanceNull(obj, name);
        }

        /// <summary>
        /// If the object is null throw a NullReferenceException.
        /// </summary>
        /// <param name="obj">Any object that exists in C#, that must not be null.</param>
        /// <exception cref="NullReferenceException">If given object is null.</exception>
        private static void Internal_IsInstanceNull(object? obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException(nameof(obj));
            }
        }

        /// <summary>
        /// If the object is null throw a NullReferenceException with the given name.
        /// </summary>
        /// <param name="obj">Any object in C#, that must not be null.</param>
        /// <param name="name">Name of the given object.</param>
        /// <exception cref="NullReferenceException">If given object is null. Include the name of the given object.</exception>
        private static void Internal_IsInstanceNull(object? obj, string name)
        {
            if (obj == null)
            {
                throw new NullReferenceException(name);
            }
        }
    }
}
