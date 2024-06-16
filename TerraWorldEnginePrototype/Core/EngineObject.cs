using System.Numerics;
using TerraWorldEnginePrototype.Core.Mathematics;

namespace TerraWorldEnginePrototype.Core
{
    /// <summary>
    /// Class Object is a base class for all classes in the TerraWorld Engine, so it is the root of the class hierarchy.
    /// </summary>
    public class EngineObject
    {
        /// <summary>
        /// The name of the object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Create a new instance of the class Object.
        /// </summary>
        public EngineObject() : this(null) { }

        /// <summary>
        /// Create a new instance of the class Object with the given name and description.
        /// </summary>
        /// <param name="name">The name of created object.</param>
        /// <param name="description">The description of created object.</param>
        public EngineObject(string? name)
        {
            Name = name ?? "New" + GetType().Name;
        }
    }

    /// <summary>
    /// Class GameObject is a base class for all objects in the TerraWorld Engine that can be placed in the scene.
    /// </summary>
    public class GameObject : EngineObject
    {
        public Mesh Mesh { get; set; }

        public Transform Transform { get; set; }

        public GameObject(Mesh mesh) 
        {
            Mesh = mesh;
            Transform = new Transform();
        }
    }

    public class Light : EngineObject
    {
        public Color Color { get; set; }

        public Light(Color color)
        {
            Color = color;
        }
    }

    public class DirectionalLight : Light
    {
        public Vector3 Direction { get; set; }
        public Vector3 Position { get; set; }

        public DirectionalLight(Color color, Vector3 direction, Vector3 position) : base(color)
        {
            Direction = direction;
            Position = position;
        }
    }

    public class PointLight : Light
    {
        public Vector3 Position { get; set; }

        public PointLight(Color color, Vector3 position) : base(color)
        {
            Position = position;
        }
    }
}
