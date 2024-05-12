using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    public class GLShader : IGLObject, IDisposable
    {
        public uint Id { get; protected set; }
        public IGLObjectType Type => IGLObjectType.Shader;

        public GLShader(ShaderType type, string source)
        {
            Id = GL.CreateShader(type);
            GL.ShaderSource(Id, source);
            GL.CompileShader(Id);

            GL.GetShaderiv(Id, ShaderParameterName.CompileStatus, out bool success);
            if (!success)
            {
                char[] infoLog = new char[512];
                GL.GetShaderInfoLog(Id, 512, out _, infoLog);
                Console.WriteLine($"ERROR::SHADER::{type}::COMPILATION_FAILED\n{infoLog}");
            }
        }

        public void Dispose()
        {
            GL.DeleteShader(Id);
        }
    }
}