using System.Collections;

namespace TerraWorldEnginePrototype.Graphics
{
    public class Graph<T> : IEnumerable<GraphNode<T>>, IEnumerable
    {
        public GraphNode<T> Root { get; set; }

        public Graph(GraphNode<T> root)
        {
            Root = root ?? throw new ArgumentNullException(nameof(root), "Root cannot be null.");
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

    public class GraphEnumerator<T> : IEnumerator<GraphNode<T>>, IEnumerator, IDisposable
    {
        object? IEnumerator.Current => Current;

        public GraphNode<T>? Current { get; private set; }
        private GraphNode<T> Root { get; }
        private Queue<GraphNode<T>> Queue { get; }

        public GraphEnumerator(GraphNode<T> root)
        {
            Root = root;
            Current = null;
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

            Current = Queue.Dequeue();

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
            Current = null;
        }
    }

    public class GraphNode<T>
    {
        private static int NextID = 1;
        private static readonly object idLock = new object();

        public int ID { get; }
        public T Value { get; set; }

        public GraphNode<T>? Parent { get; set; }
        public LinkedList<GraphNode<T>> Children { get; set; }

        public GraphNode(T value, GraphNode<T>? parent = null)
        {
            lock (idLock)
            {
                ID = NextID++;
            }

            Value = value;
            Parent = parent;
            Children = new LinkedList<GraphNode<T>>();
        }

        public void AddChild(GraphNode<T> child)
        {
            Children.AddLast(child);
            child.Parent = this;
        }

        public GraphNode<T>? FindNodeById(int id)
        {
            return ID == id ? this : Children.Select(child => child.FindNodeById(id)).FirstOrDefault(node => node != null);
        }

        public GraphNode<T>? FindNodeByValue (T value)
        {
            return EqualityComparer<T>.Default.Equals(Value, value) ? this : Children.Select(child => child.FindNodeByValue(value)).FirstOrDefault(node => node != null);
        }
    }
}
