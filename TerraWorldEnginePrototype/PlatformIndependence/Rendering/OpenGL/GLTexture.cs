using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    public class GLTexture2D<T> : GLTexture2D
        where T : unmanaged
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public GLTexture2D()
        {
            Id = GL.GenTexture();
        }

        public unsafe void BufferData(T[] data, int level, PixelInternalFormat internalFormat, int border, PixelFormat pixelFormat, PixelType pixelType)
        {
            GL.BindTexture(TextureType.Texture2D, Id);
            GL.TexImage2D(TextureType.Texture2D, level, internalFormat, Width, Height, border, pixelFormat, pixelType, data);
        }

        public void SetParameter(TextureParameterName name, int value)
        {
            GL.BindTexture(TextureType.Texture2D, Id);
            GL.TexParameter(TextureType.Texture2D, name, value);
        }

        public void SetParameter(TextureParameterName name, float value)
        {
            GL.BindTexture(TextureType.Texture2D, Id);
            GL.TexParameter(TextureType.Texture2D, name, value);
        }

        public void SetParameter(TextureParameterName name, float[] value)
        {
            GL.BindTexture(TextureType.Texture2D, Id);
            GL.TexParameter(TextureType.Texture2D, name, value);
        }

        public void GenerateMipmaps()
        {
            GL.BindTexture(TextureType.Texture2D, Id);
            GL.GenerateMipmap(TextureType.Texture2D);
        }
    }

    public class GLTexture2D : IGLObject, IDisposable
    {
        public uint Id { get; protected init; }
        public IGLObjectType Type => IGLObjectType.Texture;

        public void Bind(TextureType type)
        {
            GL.BindTexture(type, Id);
        }

        public void Dispose()
        {
            GL.DeleteTexture(Id);
        }
    }
}