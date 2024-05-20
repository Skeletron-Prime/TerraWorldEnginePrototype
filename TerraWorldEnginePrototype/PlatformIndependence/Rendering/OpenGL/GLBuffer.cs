using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    public class GLBuffer<T> : GLBuffer 
        where T : unmanaged
    {
        private int size;

        public GLBuffer()
        {
            Id = GL.GenBuffer();
        }

        public unsafe void BufferData(T[] data, BufferType type, BufferUsage usage)
        {
            GL.BindBuffer(type, Id);

            if (size > data.Length * sizeof(T))
            {
                // buffer sub data to avoid reallocation
                GL.BufferSubData(type, 0, data);
            }
            else
            {
                GL.BufferData(type, data, usage);
            }

            size = data.Length * sizeof(T);
        }
    }

    public class GLBuffer : IGLObject, IDisposable
    {
        public uint Id { get; protected init; }
        public IGLObjectType Type => IGLObjectType.Buffer;

        public void Bind(BufferType target)
        {
            GL.BindBuffer(target, Id);
        }

        public void Dispose()
        {
            GL.DeleteBuffer(Id);
        }
    }
}