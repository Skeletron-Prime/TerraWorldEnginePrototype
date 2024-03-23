using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace PlatformIndependenceLayer.Rendering
{
    internal static class GL
    {
        #region Initialization
        static GL()
        {
            GetProcAddress = GetProcAddressFunction();

            LoadFunction(out glClearColor, nameof(glClearColor));
            LoadFunction(out glClear, nameof(glClear));
            LoadFunction(out glViewport, nameof(glViewport));
            LoadFunction(out glFlush, nameof(glFlush));
            LoadFunction(out glGenBuffers, nameof(glGenBuffers));
            LoadFunction(out glBindBuffer, nameof(glBindBuffer));
            LoadFunction(out glBufferData, nameof(glBufferData));
            LoadFunction(out glCreateShader, nameof(glCreateShader));
            LoadFunction(out glGetShaderiv, nameof(glGetShaderiv));
            LoadFunction(out glGetProgramiv, nameof(glGetProgramiv));
            LoadFunction(out glGetProgramInfoLog, nameof(glGetProgramInfoLog));
            LoadFunction(out glGetShaderInfoLog, nameof(glGetShaderInfoLog));
            LoadFunction(out glShaderSource, nameof(glShaderSource));
            LoadFunction(out glCompileShader, nameof(glCompileShader));
            LoadFunction(out glCreateProgram, nameof(glCreateProgram));
            LoadFunction(out glAttachShader, nameof(glAttachShader));
            LoadFunction(out glLinkProgram, nameof(glLinkProgram));
            LoadFunction(out glUseProgram, nameof(glUseProgram));
            LoadFunction(out glDeleteShader, nameof(glDeleteShader));
            LoadFunction(out glVertexAttribPointer, nameof(glVertexAttribPointer));
            LoadFunction(out glEnableVertexAttribArray, nameof(glEnableVertexAttribArray));
            LoadFunction(out glGenVertexArrays, nameof(glGenVertexArrays));
            LoadFunction(out glBindVertexArray, nameof(glBindVertexArray));
            LoadFunction(out glDrawArrays, nameof(glDrawArrays));
            LoadFunction(out glCullFace, nameof(glCullFace));
            LoadFunction(out glGetUniformLocation, nameof(glGetUniformLocation));
            LoadFunction(out glUniform4f, nameof(glUniform4f));
            LoadFunction(out glGetError, nameof(glGetError));
        }

        private static GetProcAddressDelegate GetProcAddressFunction()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                nint lib = NativeLibrary.Load("opengl32.dll");
                nint getProcAddressPtr = NativeLibrary.GetExport(lib, "wglGetProcAddress");

                return Marshal.GetDelegateForFunctionPointer<GetProcAddressDelegate>(getProcAddressPtr);
            }
            else
            {
                throw new PlatformNotSupportedException("Only Windows is supported at this time");
            }
        }

        delegate nint GetProcAddressDelegate(string procName);
        static GetProcAddressDelegate GetProcAddress;

        private static void LoadFunction<TDelegate>(out TDelegate function, string name) where TDelegate : Delegate
        {
            nint ptr = GetProcAddress(name);

            if (ptr == 0)
                ptr = NativeLibrary.GetExport(NativeLibrary.Load("opengl32.dll"), name);

            if (ptr == 0)
            {
                throw new EntryPointNotFoundException($"Could not find function {name}");
            }

            function = Marshal.GetDelegateForFunctionPointer<TDelegate>(ptr);
        }

        #endregion

        delegate void ClearColorDelegate(float red, float green, float blue, float alpha);
        static readonly ClearColorDelegate glClearColor;

        internal static void ClearColor(float red, float green, float blue, float alpha)
        {
            glClearColor(red, green, blue, alpha);
            CheckErrors();
        }

        delegate void ClearDelegate(uint mask);
        static readonly ClearDelegate glClear;

        internal static void Clear(ClearBufferMask mask)
        {
            glClear((uint)mask);
            CheckErrors();
        }

        delegate void ViewportDelegate(int x, int y, int width, int height);
        static readonly ViewportDelegate glViewport;

        internal static void Viewport(int x, int y, int width, int height)
        {
            glViewport(x, y, width, height);
            CheckErrors();
        }

        delegate void FlushDelegate();
        static readonly FlushDelegate glFlush;

        internal static void Flush()
        {
            glFlush();
            CheckErrors();
        }

        delegate void GenBuffersDelegate(int n, ref int buffers);
        static GenBuffersDelegate glGenBuffers;

        /// <summary>
        /// Generates buffer object names. This function in itself does not
        /// create any buffer but rather helps to find an unused name for a new buffer object.
        /// </summary>
        /// <param name="n">The number of buffer object names to generate</param>
        /// <param name="buffers">Array in which the generated buffer object names are stored</param>
        internal static void GenBuffers(int n, ref int buffers)
        {
            glGenBuffers(n, ref buffers);
            CheckErrors();
        }

        delegate void BindBufferDelegate(uint target, int buffer);
        static readonly BindBufferDelegate glBindBuffer;

        /// <summary>
        /// Binds a buffer object to a buffer target. What this means is that
        /// the buffer object will now be used whenever some operation requires
        /// a buffer of the specified target.
        /// </summary>
        /// <param name="target">The target to which the buffer object is bound</param>
        /// <param name="buffer">The buffer object to bind</param>
        internal static void BindBuffer(BufferTarget target, int buffer)
        {
            glBindBuffer((uint)target, buffer);
            CheckErrors();
        }

        delegate void BufferDataDelegate(uint target, int size, float[] data, uint usage);
        static readonly BufferDataDelegate glBufferData;

        internal static void BufferData(BufferTarget target, int size, float[] data, BufferUsage usage)
        {
            glBufferData((uint)target, size, data, (uint)usage);
            CheckErrors();
        }

        delegate uint CreateShaderDelegate(uint type);
        static readonly CreateShaderDelegate glCreateShader;

        internal static uint CreateShader(ShaderType type)
        {
            var shader = glCreateShader((uint)type);
            CheckErrors();
            return shader;
        }

        delegate void GetShaderivDelegate(uint shader, uint pname, out bool success);
        static readonly GetShaderivDelegate glGetShaderiv;

        internal static void GetShaderiv(uint shader, ParameterName pname, out bool success)
        {
            glGetShaderiv(shader, (uint)pname, out success);
            CheckErrors();
        }

        delegate void GetProgramivDelegate(uint program, uint pname, out bool success);
        static readonly GetProgramivDelegate glGetProgramiv;

        internal static void GetProgramiv(uint program, ParameterName pname, out bool success)
        {
            glGetProgramiv(program, (uint)pname, out success);
            CheckErrors();
        }

        delegate void GetProgramInfoLogDelegate(uint program, int maxLength, out int length, [Out] byte[] infoLog);
        static readonly GetProgramInfoLogDelegate glGetProgramInfoLog;

        internal static void GetProgramInfoLog(uint program, int maxLength, out int length, byte[] infoLog)
        {
            glGetProgramInfoLog(program, maxLength, out length, infoLog);
            CheckErrors();
        }

        delegate void GetShaderInfoLogDelegate(uint shader, int maxLength, out int length, char[] infoLog);
        static readonly GetShaderInfoLogDelegate glGetShaderInfoLog;

        internal static void GetShaderInfoLog(uint shader, int maxLength, out int length, char[] infoLog)
        {
            glGetShaderInfoLog(shader, maxLength, out length, infoLog);
            CheckErrors();
        }

        delegate void ShaderSourceDelegate(uint shader, int count, string[] source, int[] length);
        static readonly ShaderSourceDelegate glShaderSource;

        internal static void ShaderSource(uint shader, int count, string[] source, int[] length)
        {
            glShaderSource(shader, count, source, length);
            CheckErrors();
        }

        delegate void CompileShaderDelegate(uint shader);
        static readonly CompileShaderDelegate glCompileShader;

        internal static void CompileShader(uint shader)
        {
            glCompileShader(shader);
            CheckErrors();
        }

        delegate uint CreateProgramDelegate();
        static readonly CreateProgramDelegate glCreateProgram;

        internal static uint CreateProgram()
        {
            var program = glCreateProgram();
            CheckErrors();
            return program;
        }

        delegate void AttachShaderDelegate(uint program, uint shader);
        static readonly AttachShaderDelegate glAttachShader;

        internal static void AttachShader(uint program, uint shader)
        {
            glAttachShader(program, shader);
            CheckErrors();
        }

        delegate void LinkProgramDelegate(uint program);
        static readonly LinkProgramDelegate glLinkProgram;

        internal static void LinkProgram(uint program)
        {
            glLinkProgram(program);
            CheckErrors();
        }

        delegate void UseProgramDelegate(uint program);
        static readonly UseProgramDelegate glUseProgram;

        internal static void UseProgram(uint program)
        {
            glUseProgram(program);
            CheckErrors();
        }

        delegate void DeleteShaderDelegate(uint shader);
        static readonly DeleteShaderDelegate glDeleteShader;

        internal static void DeleteShader(uint shader)
        {
            glDeleteShader(shader);
            CheckErrors();
        }

        delegate void VertexAttribPointerDelegate(uint index, int size, uint type, bool normalized, int stride, int offset);
        static readonly VertexAttribPointerDelegate glVertexAttribPointer;

        internal static void VertexAttribPointer(uint index, int size, DataType type, bool normalized, int stride, int offset)
        {
            glVertexAttribPointer(index, size, (uint)type, normalized, stride, offset);
            CheckErrors();
        }

        delegate void EnableVertexAttribArrayDelegate(uint index);
        static readonly EnableVertexAttribArrayDelegate glEnableVertexAttribArray;

        internal static void EnableVertexAttribArray(uint index)
        {
            glEnableVertexAttribArray(index);
            CheckErrors();
        }

        delegate void GenVertexArraysDelegate(int n, ref uint arrays);
        static readonly GenVertexArraysDelegate glGenVertexArrays;

        internal static void GenVertexArrays(int n, ref uint arrays)
        {
            glGenVertexArrays(n, ref arrays);
            CheckErrors();
        }

        delegate void BindVertexArrayDelegate(uint array);
        static readonly BindVertexArrayDelegate glBindVertexArray;

        internal static void BindVertexArray(uint array)
        {
            glBindVertexArray(array);
            CheckErrors();
        }

        delegate void DrawArraysDelegate(uint mode, int first, int count);
        static readonly DrawArraysDelegate glDrawArrays;

        internal static void DrawArrays(DrawMode mode, int first, int count)
        {
            glDrawArrays((uint)mode, first, count);
            CheckErrors();
        }

        delegate void CullFaceDelegate(uint mode);
        static readonly CullFaceDelegate glCullFace;

        internal static void CullFace(uint mode)
        {
            glCullFace(mode);
            CheckErrors();
        }

        delegate int GetUniformLocationDelegate(uint program, string name);
        static readonly GetUniformLocationDelegate glGetUniformLocation;

        internal static int GetUniformLocation(uint program, string name)
        {
            var location = glGetUniformLocation(program, name);
            CheckErrors();
            return location;
        }

        delegate void Uniform4fDelegate(int location, float v0, float v1, float v2, float v3);
        static readonly Uniform4fDelegate glUniform4f;

        internal static void Uniform4f(int location, float v0, float v1, float v2, float v3)
        {
            glUniform4f(location, v0, v1, v2, v3);
            CheckErrors();
        }

        delegate uint GetErrorDelegate();
        static readonly GetErrorDelegate glGetError;

        [Conditional("DEBUG")]
        private static void CheckErrors()
        {
            uint error;

            while ((error = glGetError()) != 0)
            {
                Console.WriteLine($"OpenGL Error: {error}");
            }
        }

        internal enum ClearBufferMask : uint
        {
            DepthBufferBit = 0x00000100,
            StencilBufferBit = 0x00000400,
            ColorBufferBit = 0x00004000,
        }

        internal enum ShaderType : uint
        {
            FragmentShader = 0x8B30,
            VertexShader = 0x8B31,
        }

        internal enum BufferTarget : uint
        {
            ArrayBuffer = 0x8892,
            ElementArrayBuffer = 0x8893
        }

        internal enum CullFaceMode : uint
        {
            Back = 0x0405
        }

        internal enum ErrorCode : uint
        {
            NoError = 0,
            InvalidEnum = 0x0500,
            InvalidValue = 0x0501,
            InvalidOperation = 0x0502,
            StackOverflow = 0x0503,
            StackUnderflow = 0x0504,
            OutOfMemory = 0x0505
        }

        internal enum DataType : uint
        {
            Float = 0x1406,
        }

        internal enum ParameterName : uint
        {
            CompileStatus = 0x8B81,
            LinkStatus = 0x8B82,
        }

        internal enum BufferUsage : uint
        {
            StaticDraw = 0x88E4
        }

        internal enum DrawMode : uint
        {
            Triangles = 0x0004
        }
    }
}