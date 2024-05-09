using TerraWorldEnginePrototype.PlatformIndependence.Rendering.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    internal class GLShader : GraphicsShader
    {
        internal uint ID { get; private set; }
        public override bool IsDisposed { get; protected set; }

        internal GLShader(ShaderType type, string source)
        {
            ID = GL.CreateShader(type);
            GL.ShaderSource(ID, source);
            GL.CompileShader(ID);

            GL.GetShaderiv(ID, ShaderParameterName.CompileStatus, out bool success);
            if (!success)
            {
                char[] infoLog = new char[512];
                GL.GetShaderInfoLog(ID, 512, out _, infoLog);
                Console.WriteLine($"ERROR::SHADER::{type}::COMPILATION_FAILED\n{infoLog}");
            }
        }

        public override void Dispose()
        {
            if (!IsDisposed)
            {
                GL.DeleteShader(ID);
                IsDisposed = true;
            }
        }
    }
}