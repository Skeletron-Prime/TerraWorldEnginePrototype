using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    public class GLProgram : IGLObject, IDisposable
    {
        public uint Id { get; protected set; }
        public IGLObjectType Type => IGLObjectType.Program;

        private List<GLShader> Shaders = new();

        public GLProgram()
        {
            Id = GL.CreateProgram();
        }

        public void AddShader(ShaderType type, string source)
        {
            GLShader shader = new GLShader(type, source);
            Shaders.Add(shader);
        }

        public void Build()
        {
            foreach (var shader in Shaders)
            {
                GL.AttachShader(Id, shader.Id);
            }

            GL.LinkProgram(Id);

            GL.GetProgramiv(Id, ShaderParameterName.LinkStatus, out bool success);

            if (!success)
            {
                byte[] infoLog = new byte[512];
                GL.GetProgramInfoLog(Id, 512, out _, infoLog);
                Console.WriteLine($"ERROR::SHADER::PROGRAM::LINKING_FAILED\n{infoLog}");
            }

            foreach (var shader in Shaders)
            {
                shader.Dispose();
            }

            Shaders.Clear();
        }

        public void Use()
        {
            GL.UseProgram(Id);
        }

        public void Dispose()
        {
            GL.DeleteProgram(Id);
        }
    }
}