namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    public class GLVertexArray : GraphicsVertexArray
    {
        private readonly uint id;
        private bool isDisposed;

        public uint ID => id;
        protected override bool IsDisposed => isDisposed;

        public GLVertexArray()
        {
            id = GL.GenVertexArray();

            Bind();
        }

        public void Bind()
        {
            GL.BindVertexArray(id);
        }

        public override void Dispose()
        {
            if (!isDisposed)
            {
                GL.DeleteVertexArray(id);
                isDisposed = true;
            }
        }
    }
}