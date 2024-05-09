using TerraWorldEnginePrototype.PlatformIndependence.Rendering.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    public class GLBuffer<T> : GraphicsBuffer 
        where T : unmanaged
    {
        private readonly BufferTarget target;

        public uint ID { get; private set; }
        public override bool IsDisposed { get; protected set; }

        internal GLBuffer(BufferTarget target)
        {
            ID = GL.GenBuffer();
            this.target = target;

            Bind();
        }

        internal void BufferData(T[] data, BufferUsage usage)
        {
            Bind();

            GL.BufferData(target, data, usage);
        }

        public void Bind()
        {
            GL.BindBuffer(target, ID);
        }

        public override void Dispose()
        {
            if (!IsDisposed)
            {
                GL.DeleteBuffer(ID);
                IsDisposed = true;
            }
        }
    }
}