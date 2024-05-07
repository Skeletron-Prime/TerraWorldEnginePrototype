using TerraWorldEnginePrototype.PlatformIndependence.Rendering.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    internal class GLShader : GraphicsShader
    {
        private uint id;
        private bool isDisposed;

        internal uint ID => id;
        protected override bool IsDisposed => isDisposed;

        internal GLShader(ShaderType type, string source)
        {
            id = GL.CreateShader(type);
            GL.ShaderSource(id, source);
            GL.CompileShader(id);

            GL.GetShaderiv(id, ShaderParameterName.CompileStatus, out bool success);
            if (!success)
            {
                char[] infoLog = new char[512];
                GL.GetShaderInfoLog(id, 512, out _, infoLog);
                Console.WriteLine($"ERROR::SHADER::{type}::COMPILATION_FAILED\n{infoLog}");
            }
        }

        public override void Dispose()
        {
            if (!isDisposed)
            {
                GL.DeleteShader(id);
                isDisposed = true;
            }
        }
    }
}