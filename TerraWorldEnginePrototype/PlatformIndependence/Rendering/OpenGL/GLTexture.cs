using StbImageSharp;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    public class GLTexture : GraphicsTexture
    {
        private ImageResult image;

        internal uint ID { get; private set; }
        public override bool IsDisposed { get; protected set; }
        public override TextureType Type { get; protected set; }

        internal GLTexture(TextureType type)
        {
            ID = GL.GenTexture();
            Type = type;
        }

        public static GLTexture LoadFromFile(string path, TextureType type)
        {
            GLTexture texture = new GLTexture(type);
            texture.image = ImageResult.FromStream(File.OpenRead(path), ColorComponents.RedGreenBlueAlpha);

            texture.Bind();
            texture.SetData(texture.image.Data, texture.image.Width, texture.image.Height);
            texture.SetFilter(TextureMinFilter.Linear, TextureMagFilter.Linear);
            texture.SetWrap(TextureWrapMode.Repeat, TextureWrapMode.Repeat);
            texture.GenerateMipmaps();

            return texture;
        }

        public void Bind()
        {
            GL.BindTexture(Type, ID);
        }

        public void SetData(byte[] data, int width, int height)
        {
            Bind();
            GL.TexImage2D(Type, 0, PixelInternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, data);
        }

        public void SetFilter(TextureMinFilter minFilter, TextureMagFilter magFilter)
        {
            Bind();
            GL.TexParameter(Type, TextureParameterName.TextureMinFilter, (int)minFilter);
            GL.TexParameter(Type, TextureParameterName.TextureMagFilter, (int)magFilter);
        }

        public void SetWrap(TextureWrapMode wrapS, TextureWrapMode wrapT)
        {
            Bind();
            GL.TexParameter(Type, TextureParameterName.TextureWrapS, (int)wrapS);
            GL.TexParameter(Type, TextureParameterName.TextureWrapT, (int)wrapT);
        }

        public void GenerateMipmaps()
        {
            Bind();
            GL.GenerateMipmap(Type);
        }

        public void Use(TextureUnit unit = TextureUnit.Texture0)
        {
            GL.ActiveTexture(unit);
            Bind();
        }

        public override void Dispose()
        {
            if (!IsDisposed)
            {
                GL.DeleteTexture(ID);
                IsDisposed = true;
            }
        }
    }
}