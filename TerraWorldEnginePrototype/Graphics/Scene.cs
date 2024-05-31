using TerraWorldEnginePrototype.Core;

namespace TerraWorldEnginePrototype.Graphics
{
    public class Scene
    {
        public Camera CurrentCamera { get; set; }

        public Graph<EngineObject> SceneGraph { get; } = new Graph<EngineObject>(new GraphNode<EngineObject>(new EngineObject()));

        public Scene(Camera camera)
        {
            CurrentCamera = camera;
        }

        public void AddObject(EngineObject obj)
        {
            SceneGraph.Root.AddChild(new GraphNode<EngineObject>(obj));
        }
    }
}
