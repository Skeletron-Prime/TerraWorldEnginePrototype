using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    public class GLFramebuffer<T> : GLFramebuffer
        where T : unmanaged
    {
        private readonly GLTexture2D<T> texture;

        public GLFramebuffer()
        {
            Id = GL.GenFramebuffer();
            texture = new GLTexture2D<T>();
        }

        public void AttachTexture(FramebufferAttachment attachment, TextureType textureType)
        {
            Bind(FramebufferType.Framebuffer);
            texture.Bind(textureType);
            GL.FramebufferTexture2D(FramebufferType.Framebuffer, attachment, textureType, texture.Id, 0);
        }
    }

    public class GLFramebuffer : IGLObject, IDisposable
    {
        public uint Id { get; protected init; }
        public IGLObjectType Type => IGLObjectType.Framebuffer;

        public void Bind(FramebufferType type)
        {
            GL.BindFramebuffer(type, Id);
        }

        public void Dispose()
        {
            GL.DeleteFramebuffer(Id);
        }
    }
}