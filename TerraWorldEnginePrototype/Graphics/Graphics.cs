using System.Collections;
using System.Numerics;
using TerraWorldEnginePrototype.Core.Mathematics;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering;

namespace TerraWorldEnginePrototype.Graphics
{
    public static class Graphics
    {
        public static Renderer Renderer { get; private set; }
        public static Scene Scene { get; private set; }

        static Graphics()
        {
            Renderer = Renderer.Create();
            Scene = new([]);
        }

        public static void DrawScene()
        {
            foreach (var engineObject in Scene.EngineObjects)
            {
                Renderer.Draw(engineObject);
            }
        }
    }

    public class Scene
    {
        public List<EngineObject> EngineObjects { get; }

        public Scene(EngineObject[] engineObjects)
        {
            EngineObjects = new(engineObjects);
        }
    }

    public class EngineObject
    {
        public Mesh Mesh { get; }
        public Transform Transform { get; }

        public EngineObject(Mesh mesh, Transform transform)
        {
            Mesh = mesh;
            Transform = transform;
        }
    }

    public class CubeObject : EngineObject
    {
        public static Mesh SMesh { get; } = new Mesh
            {
                Vertices = [
                // Front face
                new Vector3(-1.0f, -1.0f,  1.0f),
                new Vector3( 1.0f, -1.0f,  1.0f),
                new Vector3( 1.0f,  1.0f,  1.0f),
                new Vector3( 1.0f,  1.0f,  1.0f),
                new Vector3(-1.0f,  1.0f,  1.0f),
                new Vector3(-1.0f, -1.0f,  1.0f),

                // Back face
                new Vector3(-1.0f, -1.0f, -1.0f),
                new Vector3(-1.0f,  1.0f, -1.0f),
                new Vector3( 1.0f,  1.0f, -1.0f),
                new Vector3( 1.0f,  1.0f, -1.0f),
                new Vector3( 1.0f, -1.0f, -1.0f),
                new Vector3(-1.0f, -1.0f, -1.0f),

                // Left face
                new Vector3(-1.0f,  1.0f,  1.0f),
                new Vector3(-1.0f,  1.0f, -1.0f),
                new Vector3(-1.0f, -1.0f, -1.0f),
                new Vector3(-1.0f, -1.0f, -1.0f),
                new Vector3(-1.0f, -1.0f,  1.0f),
                new Vector3(-1.0f,  1.0f,  1.0f),

                // Right face
                new Vector3( 1.0f,  1.0f,  1.0f),
                new Vector3( 1.0f, -1.0f, -1.0f),
                new Vector3( 1.0f,  1.0f, -1.0f),
                new Vector3( 1.0f, -1.0f, -1.0f),
                new Vector3( 1.0f,  1.0f,  1.0f),
                new Vector3( 1.0f, -1.0f,  1.0f),

                // Top face
                new Vector3(-1.0f,  1.0f,  1.0f),
                new Vector3( 1.0f,  1.0f,  1.0f),
                new Vector3( 1.0f,  1.0f, -1.0f),
                new Vector3( 1.0f,  1.0f, -1.0f),
                new Vector3(-1.0f,  1.0f, -1.0f),
                new Vector3(-1.0f,  1.0f,  1.0f),

                // Bottom face
                new Vector3(-1.0f, -1.0f,  1.0f),
                new Vector3(-1.0f, -1.0f, -1.0f),
                new Vector3( 1.0f, -1.0f, -1.0f),
                new Vector3( 1.0f, -1.0f, -1.0f),
                new Vector3( 1.0f, -1.0f,  1.0f),
                new Vector3(-1.0f, -1.0f,  1.0f)
            ],

                Colors =
            [
                // Front face
                Color.Red,
                Color.Red,
                Color.Red,
                Color.Red,
                Color.Red,
                Color.Red,

                // Back face
                Color.Green,
                Color.Green,
                Color.Green,
                Color.Green,
                Color.Green,
                Color.Green,

                // Left face
                Color.Blue,
                Color.Blue,
                Color.Blue,
                Color.Blue,
                Color.Blue,
                Color.Blue,

                // Right face
                Color.Yellow,
                Color.Yellow,
                Color.Yellow,
                Color.Yellow,
                Color.Yellow,
                Color.Yellow,

                // Top face
                Color.Purple,
                Color.Purple,
                Color.Purple,
                Color.Purple,
                Color.Purple,
                Color.Purple,

                // Bottom face
                Color.Cyan,
                Color.Cyan,
                Color.Cyan,
                Color.Cyan,
                Color.Cyan,
                Color.Cyan
            ]
    };

        public CubeObject(Transform transform) : base(SMesh, transform) { }
    }

    public struct Transform
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public Vector3 Scale { get; set; }
    }

    public class Graph<T> : IEnumerable<GraphNode<T>>
    {
        public GraphNode<T>? Root { get; set; }

        public Graph(GraphNode<T> root)
        {
            Root = root;
        }

        public IEnumerator<GraphNode<T>> GetEnumerator()
        {
            return new GraphEnumerator<T>(Root);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class GraphEnumerator<T> : IEnumerator<GraphNode<T>>
    {
        public GraphNode<T> Current => current!;
        object? IEnumerator.Current => current;

        private GraphNode<T>? current;
        private GraphNode<T>? Root { get; }
        private Queue<GraphNode<T>> Queue { get; }

        public GraphEnumerator(GraphNode<T>? root)
        {
            Root = root;
            current = null;
            Queue = new Queue<GraphNode<T>>();
            Queue.Enqueue(Root);
        }

        public void Dispose() { }

        public bool MoveNext()
        {
            if (Queue.Count == 0)
            {
                return false;
            }

            current = Queue.Dequeue();

            foreach (var child in Current.Children)
            {
                Queue.Enqueue(child);
            }

            return true;
        }

        public void Reset()
        {
            Queue.Clear();
            Queue.Enqueue(Root);
        }
    }

    public class GraphNode<T>
    {
        private static int NextID = 1;

        public int ID { get; }
        public T Value { get; set; }

        public GraphNode<T>? Parent { get; set; }
        public LinkedList<GraphNode<T>> Children { get; set; }

        public GraphNode(T value, GraphNode<T>? parent = null)
        {
            ID = NextID++;
            Value = value;
            Parent = parent;
            Children = new LinkedList<GraphNode<T>>();
        }

        public void AddChild(T value)
        {
            AddChild(new GraphNode<T>(value, this));
        }

        public void AddChild(GraphNode<T> child)
        {
            Children.AddLast(child);
            child.Parent = this;
        }

        public void RemoveChild(GraphNode<T> child)
        {
            Children.Remove(child);
            child.Parent = null;
        }

        public bool IsRoot()
        {
            return Parent == null;
        }

        public bool IsLeaf()
        {
            return Children.Count == 0;
        }
    }
}
