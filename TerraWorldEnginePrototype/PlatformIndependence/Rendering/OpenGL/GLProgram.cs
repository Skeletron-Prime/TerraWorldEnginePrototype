using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    public class GLProgram : IGLObject, IDisposable
    {
        public uint Id { get; protected set; }
        public IGLObjectType Type => IGLObjectType.Program;

        public GLProgram(Dictionary<ShaderType, string> shaderSources)
        {
            Id = GL.CreateProgram();

            GLShader[] shaders = new GLShader[shaderSources.Count];

            for (int i = 0; i < shaderSources.Count; i++)
            {
                shaders[i] = new GLShader(shaderSources.ElementAt(i).Key, shaderSources.ElementAt(i).Value);
                GL.AttachShader(Id, shaders[i].Id);
            }

            GL.LinkProgram(Id);

            GL.GetProgramiv(Id, ShaderParameterName.LinkStatus, out bool success);

            if (!success)
            {
                byte[] infoLog = new byte[512];
                GL.GetProgramInfoLog(Id, 512, out _, infoLog);
                Console.WriteLine($"ERROR::SHADER::PROGRAM::LINKING_FAILED\n{infoLog}");
            }

            foreach (var shader in shaders)
            {
                shader.Dispose();
            }
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