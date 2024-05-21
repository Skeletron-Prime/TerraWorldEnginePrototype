using System.Diagnostics;
using System.Runtime.InteropServices;
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
                Stride = 3
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
                Stride = 4
            });
        }

        public void AddAttribute16f()
        {
            Attributes.Add(new VertexAttribute
            {
                Index = (uint)Attributes.Count,
                Size = 4,
                Type = DataType.Float,
                Normalized = false,
                Stride = 16
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
                switch (attribute.Type)
                {
                    case DataType.Byte:
                    case DataType.UnsignedByte:
                        stride += sizeof(byte) * attribute.Stride;
                        break;
                    case DataType.Short:
                    case DataType.UnsignedShort:
                        stride += sizeof(ushort) * attribute.Stride;
                        break;
                    case DataType.Int:
                    case DataType.UnsignedInt:
                        stride += sizeof(int) * attribute.Stride;
                        break;
                    case DataType.Float:
                        stride += sizeof(float) * attribute.Stride;
                        break;
                    case DataType.Double:
                        stride += sizeof(double) * attribute.Stride;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            foreach (var (index, size, type, normalized) in Attributes)
            {
                GL.VertexAttribPointer(index, size, type, normalized, stride, offset);
                GL.EnableVertexAttribArray(index);

                switch (type)
                {
                    case DataType.Byte:
                    case DataType.UnsignedByte:
                        offset += sizeof(byte) * size;
                        break;
                    case DataType.Short:
                    case DataType.UnsignedShort:
                        offset += sizeof(ushort) * size;
                        break;
                    case DataType.Int:
                    case DataType.UnsignedInt:
                        offset += sizeof(int) * size;
                        break;
                    case DataType.Float:
                        offset += sizeof(float) * size;
                        break;
                    case DataType.Double:
                        offset += sizeof(double) * size;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
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
            public int Stride { get; set; }

            //deconstructor

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