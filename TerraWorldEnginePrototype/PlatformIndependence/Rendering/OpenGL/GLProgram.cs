using TerraWorldEnginePrototype.PlatformIndependence.Rendering.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    internal class GLProgram : GraphicsProgram
    {
        internal uint ID { get; private set; }
        public override bool IsDisposed { get; protected set; }

        internal GLProgram(Dictionary<ShaderType, string> shadersources)
        {
            GLShader[] shaders = new GLShader[shadersources.Count];

            ID = GL.CreateProgram();

            for (int i = 0; i < shadersources.Count; i++)
            {
                shaders[i] = new GLShader(shadersources.ElementAt(i).Key, shadersources.ElementAt(i).Value);

                GL.AttachShader(ID, shaders[i].ID);
            }

            GL.LinkProgram(ID);

            GL.GetProgramiv(ID, ShaderParameterName.LinkStatus, out bool success);
            if (!success)
            {
                byte[] infoLog = new byte[512];
                GL.GetProgramInfoLog(ID, 512, out _, infoLog);
                Console.WriteLine($"ERROR::SHADER::PROGRAM::LINKING_FAILED\n{infoLog}");
            }

            foreach (GLShader shader in shaders)
            {
                shader.Dispose();
            }

            GL.UseProgram(ID);
        }

        public override void Dispose()
        {
            if (!IsDisposed)
            {
                GL.DeleteProgram(ID);
                IsDisposed = true;
            }
        }
    }
}