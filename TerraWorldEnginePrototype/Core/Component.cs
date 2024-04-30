namespace TerraWorldEnginePrototype.Core
{
    /// <summary>
    /// Component class is basically a class that is used to add functionality to a GameObject.
    /// </summary>
    public abstract class Component : Object
    {
        /// <summary>
        /// The GameObject this component is attached to. It is null if the component is not attached to any GameObject.
        /// </summary>
        public GameObject? GameObject { get; protected set; }

        public Component()
        {
        }

        public Component(GameObject gameObject)
        {
            GameObject = gameObject;
        }
    }
}
