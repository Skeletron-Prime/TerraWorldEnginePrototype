using System.Numerics;
namespace TerraWorldEnginePrototype.Core.Mathematics
{
    public class Mesh
    {
        private bool isChanged = true;

        public readonly bool IsWriteable = true;
        public readonly bool IsReadable = true;

        private Vector3[]? vertices;
        private Color[]? colors;

        public Vector3[] Vertices
        {
            get => vertices ?? [];
            set
            {
                if (!IsWriteable)
                    return;

                var needChange = vertices == null || vertices.Length != value.Length;
                vertices = value;
                isChanged = true;

                if (needChange)
                {
                    colors = null;
                }
            }
        }

        public Color[] Colors
        {
            get => ReadVertexData(colors ?? []);
            set => WriteVertexData(ref colors, value, value.Length);
        }

        public bool IsChanged
        {
            get => isChanged;
            set => isChanged = value;
        }

        public int VertexCount => vertices?.Length ?? 0;

        public bool HasColors => (colors?.Length ?? 0) > 0;

        public Mesh() { }

        private T ReadVertexData<T>(T value)
        {
            if (!IsReadable)
            {
                throw new Exception("Mesh is not readable");
            }

            return value;
        }

        private void WriteVertexData<T>(ref T target, T value, int length)
        {
            if (!IsWriteable)
            {
                throw new System.Exception("Mesh is not writeable");
            }

            if (value == null || length <= 0 || length != (vertices?.Length ?? 0))
            {
                throw new Exception("Array length should match vertices length");
            }

            isChanged = true;
            target = value;
        }
    }
}
