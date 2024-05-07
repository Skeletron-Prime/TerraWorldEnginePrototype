using TerraWorldEnginePrototype.PlatformIndependence.Rendering.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    public class GLBuffer<T> : GraphicsBuffer 
        where T : unmanaged
    {
        private readonly uint id;
        private readonly BufferTarget target;
        private bool isDisposed;

        public uint ID => id;
        protected override bool IsDisposed => isDisposed;

        internal GLBuffer(BufferTarget target)
        {
            id = GL.GenBuffer();
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
            GL.BindBuffer(target, id);
        }

        public override void Dispose()
        {
            if (!isDisposed)
            {
                GL.DeleteBuffer(id);
                isDisposed = true;
            }
        }
    }
}