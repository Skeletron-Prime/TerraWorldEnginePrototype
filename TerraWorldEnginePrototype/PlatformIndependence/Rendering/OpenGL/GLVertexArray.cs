namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    public class GLVertexArray : GraphicsVertexArray
    {
        public uint ID { get; private set; }
        public override bool IsDisposed { get; protected set; }

        public GLVertexArray()
        {
            ID = GL.GenVertexArray();

            Bind();
        }

        public void Bind()
        {
            GL.BindVertexArray(ID);
        }

        public override void Dispose()
        {
            if (!IsDisposed)
            {
                GL.DeleteVertexArray(ID);
                IsDisposed = true;
            }
        }
    }
}