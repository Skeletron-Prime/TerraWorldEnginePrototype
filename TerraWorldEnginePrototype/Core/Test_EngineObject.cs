using System.Numerics;
using TerraWorldEnginePrototype.Core.Mathematics;

namespace TerraWorldEnginePrototype.Core
{
    /// <summary>
    /// Class Object is a base class for all classes in the TerraWorld Engine, so it is the root of the class hierarchy.
    /// </summary>
    public class Test_EngineObject
    {
        /// <summary>
        /// The next ID that will be assigned to the next object.
        /// </summary>
        static int NextID = 1;

        /// <summary>
        /// The ID of the object.
        /// </summary>
        private readonly int id;

        /// <summary>
        /// The ID of the object.
        /// </summary>
        public int ID => id;

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
            id = NextID++;
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

    /// <summary>
    /// Class GameObject is a base class for all objects in the TerraWorld Engine that can be placed in the scene.
    /// </summary>
    public class Test_GameObject : Test_EngineObject
    {
        private readonly List<Component> components = [];

        public List<Component> Components => components;

        public Test_GameObject(string name = "NewGameObject", string description = "New GameObject") : base(name, description) { }

        public void AddComponent<T>() where T : Component
        {
            T component = (T)Activator.CreateInstance(typeof(T));
            component.AttachToGameObject(this);

            components.Add(component);
        }

        public T? GetComponent<T>() where T : Component
        {
            foreach (var component in components)
            {
                if (component is T comp)
                {
                    return comp;
                }
            }

            return null;
        }
    }

    /// <summary>
    /// Class Component is a base class for all components in the TerraWorld Engine.
    /// </summary>
    public class Component : Test_EngineObject
    {
        private Test_GameObject? gameObject;

        public Test_GameObject? GameObject => gameObject;

        public Component(string name = "NewComponent", string description = "New Component") : base(name, description) { }

        public void AttachToGameObject(Test_GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }

    /// <summary>
    /// Class Actor is a base class for all classes, which permanently act in the scene and should be updated.
    /// </summary>
    public class Actor : Test_EngineObject
    {
        public Actor(string name = "NewActor", string description = "New Actor") : base(name, description) { }

        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void PhysicsUpdate() { }
        public virtual void OnRenderObject() { }
        public virtual void OnDestroy() { }
    }

    /// <summary>
    /// Class MeshRenderer is a class that renders a mesh in the main scene.
    /// </summary>
    public class MeshRenderer : Actor
    {
        public Mesh mesh;

        public MeshRenderer(Mesh mesh, string name = "NewMeshRenderer", string description = "New Mesh Renderer") : base(name, description)
        {
            this.mesh = mesh;
        }

        public override void OnRenderObject()
        {

        }
    }
}
