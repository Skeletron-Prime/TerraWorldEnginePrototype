using TerraWorldEnginePrototype.PlatformIndependence.Rendering.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    internal class GLProgram : GraphicsProgram
    {
        private uint id;
        private bool isDisposed;

        internal uint ID => id;
        protected override bool IsDisposed => isDisposed;

        internal GLProgram(Dictionary<ShaderType, string> shadersources)
        {
            GLShader[] shaders = new GLShader[shadersources.Count];

            id = GL.CreateProgram();

            for (int i = 0; i < shadersources.Count; i++)
            {
                GLShader shader = new GLShader(shadersources.ElementAt(i).Key, shadersources.ElementAt(i).Value);

                GL.AttachShader(id, shader.ID);
            }

            GL.LinkProgram(id);

            GL.GetProgramiv(id, ShaderParameterName.LinkStatus, out bool success);
            if (!success)
            {
                byte[] infoLog = new byte[512];
                GL.GetProgramInfoLog(id, 512, out _, infoLog);
                Console.WriteLine($"ERROR::SHADER::PROGRAM::LINKING_FAILED\n{infoLog}");
            }

            foreach (GLShader shader in shaders)
            {
                shader.Dispose();
            }

            GL.UseProgram(id);
        }

        public override void Dispose()
        {
            if (!isDisposed)
            {
                GL.DeleteProgram(id);
                isDisposed = true;
            }
        }
    }
}