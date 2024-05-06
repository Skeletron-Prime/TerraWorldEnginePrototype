using TerraWorldEnginePrototype.PlatformIndependence.Rendering.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    public readonly struct Buffer
    {
        private readonly uint handle;
        private readonly BufferTarget target;

        public readonly uint Handle => handle;

        internal Buffer(BufferTarget target)
        {
            handle = GL.GenBuffer();
            this.target = target;

            GL.BindBuffer(target, handle);
        }

        internal void BufferData<T>(T[] data, BufferUsage usage) where T : unmanaged
        {
            Bind();

            GL.BufferData(target, data, usage);
        }

        public void Bind()
        {
            GL.BindBuffer(target, handle);
        }

        public void Unbind()
        {
            GL.BindBuffer(target, 0);
        }
    }
}