namespace CoreEnginePrototype
{
    /// <summary>
    /// GameObject is the main class out of which every object in the game is built.
    /// </summary>
    public class GameObject : Object
    {
        /// <summary>
        /// Name of the GameObject. Default is "GameObject".
        /// </summary>
        public string Name { get; set; } = "GameObject";

        public GameObject()
        {
        }

        public GameObject(string name)
        {
            Name = name;
        }
    }
}
