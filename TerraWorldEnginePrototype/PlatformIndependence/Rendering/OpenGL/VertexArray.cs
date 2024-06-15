using System.Diagnostics;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    public class VertexArray : IGLObject, IDisposable
    {
        public uint Id { get; protected init; }
        public IGLObjectType Type => IGLObjectType.VertexArray;

        private readonly List<VertexAttribute> Attributes = [];
        private bool IsBuilt = false;

        public VertexArray()
        {
            Id = GL.GenVertexArray();
        }

        public void AddAttribute3f()
        {
            Attributes.Add(new VertexAttribute
            {
                Index = (uint)Attributes.Count,
                Size = 3,
                Type = DataType.Float,
                Normalized = false,
            });
        }

        public void AddAttribute4f()
        {
            Attributes.Add(new VertexAttribute
            {
                Index = (uint)Attributes.Count,
                Size = 4,
                Type = DataType.Float,
                Normalized = false,
            });
        }

        public unsafe void Build(GLBuffer buffer) 
        {
            GL.BindVertexArray(Id);

            buffer.Bind(BufferType.ArrayBuffer);

            int stride = 0;
            int offset = 0;

            foreach (var attribute in Attributes)
            {
                stride += sizeof(float) * attribute.Size;
            }

            foreach (var (index, size, type, normalized) in Attributes)
            {
                GL.VertexAttribPointer(index, size, type, normalized, stride, offset);
                GL.EnableVertexAttribArray(index);

                offset += sizeof(float) * size;
            }

            GL.BindVertexArray(0);
            IsBuilt = true;

            Attributes.Clear();
        }

        public void Bind()
        {
            Debug.Assert(IsBuilt, "VertexArray must be built before binding.");
            GL.BindVertexArray(Id);
        }

        public void Unbind()
        {
            Debug.Assert(IsBuilt, "VertexArray must be built before unbinding.");
            GL.BindVertexArray(0);
        }

        public void Dispose()
        {
            GL.DeleteVertexArray(Id);
        }

        private struct VertexAttribute
        {
            public uint Index { get; set; }
            public int Size { get; set; }
            public DataType Type { get; set; }
            public bool Normalized { get; set; }

            public void Deconstruct(out uint index, out int size, out DataType type, out bool normalized)
            {
                index = Index;
                size = Size;
                type = Type;
                normalized = Normalized;
            }
        }
    }
}