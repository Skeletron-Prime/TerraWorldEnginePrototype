namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    public readonly struct VertexArray
    {
        private readonly uint handle;

        public uint Handle => handle;

        public VertexArray()
        {
            handle = GL.GenVertexArray();

            GL.BindVertexArray(handle);
        }

        public void Bind()
        {
            GL.BindVertexArray(handle);
        }

        public void Unbind()
        {
            GL.BindVertexArray(0);
        }
    }
}