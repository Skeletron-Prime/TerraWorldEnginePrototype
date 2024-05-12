using System.Diagnostics;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    public class VertexArray : IGLObject, IDisposable
    {
        public uint Id { get; protected init; }
        public IGLObjectType Type => IGLObjectType.VertexArray;

        private readonly List<VertexAttribute> Attributes = new();
        private bool IsBuilt = false;

        public VertexArray()
        {
            Id = GL.GenVertexArray();
        }

        public void AddAttribute(GLBuffer buffer, uint index, int size, DataType type, bool normalized, int stride, int offset)
        {
            Attributes.Add(new VertexAttribute
            {
                Buffer = buffer,
                Index = index,
                Size = size,
                Type = type,
                Normalized = normalized,
                Stride = stride,
                Offset = offset
            });
        }

        public void Build()
        {
            GL.BindVertexArray(Id);

            foreach (var (buffer, index, size, type, normalized, stride, offset) in Attributes)
            {
                buffer.Bind(BufferType.ArrayBuffer);
                GL.VertexAttribPointer(index, size, type, normalized, stride, offset);
                GL.EnableVertexAttribArray(index);
            }

            GL.BindVertexArray(0);
            IsBuilt = true;
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
            public GLBuffer Buffer { get; set; }
            public uint Index { get; set; }
            public int Size { get; set; }
            public DataType Type { get; set; }
            public bool Normalized { get; set; }
            public int Stride { get; set; }
            public int Offset { get; set; }

            //deconstructor

            public void Deconstruct(out GLBuffer buffer, out uint index, out int size, out DataType type, out bool normalized, out int stride, out int offset)
            {
                buffer = Buffer;
                index = Index;
                size = Size;
                type = Type;
                normalized = Normalized;
                stride = Stride;
                offset = Offset;
            }
        }
    }
}