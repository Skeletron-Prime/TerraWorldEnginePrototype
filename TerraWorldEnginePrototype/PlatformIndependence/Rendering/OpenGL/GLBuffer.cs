using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    public class GLBuffer<T> : GLBuffer 
        where T : unmanaged
    {
        public GLBuffer()
        {
            Id = GL.GenBuffer();
        }

        public void BufferData(T[] data, BufferUsage usage)
        {
            GL.BindBuffer(BufferType.ArrayBuffer, Id);
            GL.BufferData(BufferType.ArrayBuffer, data, usage);
        }

        public void BufferSubData(int offset, T[] data)
        {
            GL.BindBuffer(BufferType.ArrayBuffer, Id);
            GL.BufferSubData(BufferType.ArrayBuffer, offset, data);
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