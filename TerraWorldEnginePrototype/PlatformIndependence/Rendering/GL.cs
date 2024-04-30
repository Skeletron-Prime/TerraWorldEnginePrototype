using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering
{
    internal static class GL
    {
        #region Initialization
        static GL()
        {
            GetProcAddress = GetProcAddressFunction();

            LoadFunction(out glClearColor, nameof(glClearColor));
            LoadFunction(out glClear, nameof(glClear));
            LoadFunction(out glBindBuffer, nameof(glBindBuffer));
            LoadFunction(out glBindTexture, nameof(glBindTexture));
            LoadFunction(out glAttachShader, nameof(glAttachShader));
            LoadFunction(out glBindVertexArray, nameof(glBindVertexArray));
            LoadFunction(out glBufferData, nameof(glBufferData));
            LoadFunction(out glCompileShader, nameof(glCompileShader));
            LoadFunction(out glCreateProgram, nameof(glCreateProgram));
            LoadFunction(out glCreateShader, nameof(glCreateShader));
            LoadFunction(out glCullFace, nameof(glCullFace));
            LoadFunction(out glDeleteBuffers, nameof(glDeleteBuffers));
            LoadFunction(out glDeleteShader, nameof(glDeleteShader));
            LoadFunction(out glDeleteTextures, nameof(glDeleteTextures));
            LoadFunction(out glDrawArrays, nameof(glDrawArrays));
            LoadFunction(out glDrawElements, nameof(glDrawElements));
            LoadFunction(out glEnableVertexAttribArray, nameof(glEnableVertexAttribArray));
            LoadFunction(out glFlush, nameof(glFlush));
            LoadFunction(out glGenBuffers, nameof(glGenBuffers));
            LoadFunction(out glGenTextures, nameof(glGenTextures));
            LoadFunction(out glGenVertexArrays, nameof(glGenVertexArrays));
            LoadFunction(out glGetProgramInfoLog, nameof(glGetProgramInfoLog));
            LoadFunction(out glGetProgramiv, nameof(glGetProgramiv));
            LoadFunction(out glGetShaderInfoLog, nameof(glGetShaderInfoLog));
            LoadFunction(out glGetShaderiv, nameof(glGetShaderiv));
            LoadFunction(out glGetUniformLocation, nameof(glGetUniformLocation));
            LoadFunction(out glLinkProgram, nameof(glLinkProgram));
            LoadFunction(out glShaderSource, nameof(glShaderSource));
            LoadFunction(out glTexImage2D, nameof(glTexImage2D));
            LoadFunction(out glTexImage3D, nameof(glTexImage3D));
            LoadFunction(out glTexParameterf, nameof(glTexParameterf));
            LoadFunction(out glTexParameterfv, nameof(glTexParameterfv));
            LoadFunction(out glTexParameteri, nameof(glTexParameteri));
            LoadFunction(out glTexSubImage2D, nameof(glTexSubImage2D));
            LoadFunction(out glUseProgram, nameof(glUseProgram));
            LoadFunction(out glVertexAttribPointer, nameof(glVertexAttribPointer));
            LoadFunction(out glViewport, nameof(glViewport));
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

        #region Attach Shader

        delegate void AttachShaderDelegate(uint program, uint shader);
        static readonly AttachShaderDelegate glAttachShader;

        internal static void AttachShader(uint program, uint shader)
        {
            glAttachShader(program, shader);
            CheckErrors();
        }

        #endregion

        #region Bind Buffer

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

        #endregion

        #region Bind Texture

        delegate void BindTextureDelegate(uint target, uint texture);
        static readonly BindTextureDelegate glBindTexture;

        internal static void BindTexture(TextureTarget target, uint texture)
        {
            glBindTexture((uint)target, texture);
            CheckErrors();
        }

        #endregion

        #region Bind Vertex Array

        delegate void BindVertexArrayDelegate(uint array);
        static readonly BindVertexArrayDelegate glBindVertexArray;

        internal static void BindVertexArray(uint array)
        {
            glBindVertexArray(array);
            CheckErrors();
        }

        #endregion

        #region Buffer Data

        delegate void BufferDataDelegate(uint target, int size, float[] data, uint usage);
        static readonly BufferDataDelegate glBufferData;

        internal static void BufferData(BufferTarget target, int size, float[] data, BufferUsage usage)
        {
            glBufferData((uint)target, size, data, (uint)usage);
            CheckErrors();
        }

        #endregion

        #region Clear

        delegate void ClearDelegate(uint mask);
        static readonly ClearDelegate glClear;

        internal static void Clear(ClearBufferMask mask)
        {
            glClear((uint)mask);
            CheckErrors();
        }

        #endregion

        #region Clear Color

        delegate void ClearColorDelegate(float red, float green, float blue, float alpha);
        static readonly ClearColorDelegate glClearColor;

        internal static void ClearColor(float red, float green, float blue, float alpha)
        {
            glClearColor(red, green, blue, alpha);
            CheckErrors();
        }

        #endregion

        #region Compile Shader

        delegate void CompileShaderDelegate(uint shader);
        static readonly CompileShaderDelegate glCompileShader;

        internal static void CompileShader(uint shader)
        {
            glCompileShader(shader);
            CheckErrors();
        }

        #endregion

        #region Create Program

        delegate uint CreateProgramDelegate();
        static readonly CreateProgramDelegate glCreateProgram;

        internal static uint CreateProgram()
        {
            var program = glCreateProgram();
            CheckErrors();
            return program;
        }

        #endregion

        #region Create Shader

        delegate uint CreateShaderDelegate(uint type);
        static readonly CreateShaderDelegate glCreateShader;

        internal static uint CreateShader(ShaderType type)
        {
            var shader = glCreateShader((uint)type);
            CheckErrors();
            return shader;
        }

        #endregion

        #region Cull Face

        delegate void CullFaceDelegate(uint mode);
        static readonly CullFaceDelegate glCullFace;

        internal static void CullFace(uint mode)
        {
            glCullFace(mode);
            CheckErrors();
        }

        #endregion

        #region Delete Buffers

        delegate void DeleteBuffersDelegate(int n, ref int buffers);
        static readonly DeleteBuffersDelegate glDeleteBuffers;

        internal static void DeleteBuffers(int n, ref int buffers)
        {
            glDeleteBuffers(n, ref buffers);
            CheckErrors();
        }

        #endregion

        #region Delete Shader

        delegate void DeleteShaderDelegate(uint shader);
        static readonly DeleteShaderDelegate glDeleteShader;

        internal static void DeleteShader(uint shader)
        {
            glDeleteShader(shader);
            CheckErrors();
        }

        #endregion

        #region Delete Textures

        delegate void DeleteTexturesDelegate(int n, ref uint textures);
        static readonly DeleteTexturesDelegate glDeleteTextures;

        internal static void DeleteTextures(int n, ref uint textures)
        {
            glDeleteTextures(n, ref textures);
            CheckErrors();
        }

        #endregion

        #region Draw Arrays

        delegate void DrawArraysDelegate(uint mode, int first, int count);
        static readonly DrawArraysDelegate glDrawArrays;

        internal static void DrawArrays(DrawMode mode, int first, int count)
        {
            glDrawArrays((uint)mode, first, count);
            CheckErrors();
        }

        #endregion

        #region Draw Elements

        delegate void DrawElementsDelegate(uint mode, int count, uint type, int indices);
        static readonly DrawElementsDelegate glDrawElements;

        internal static void DrawElements(DrawMode mode, int count, uint type, int indices)
        {
            glDrawElements((uint)mode, count, type, indices);
            CheckErrors();
        }

        #endregion

        #region Enable Vertex Attribute Array

        delegate void EnableVertexAttribArrayDelegate(uint index);
        static readonly EnableVertexAttribArrayDelegate glEnableVertexAttribArray;

        internal static void EnableVertexAttribArray(uint index)
        {
            glEnableVertexAttribArray(index);
            CheckErrors();
        }

        #endregion

        #region Flush

        delegate void FlushDelegate();
        static readonly FlushDelegate glFlush;

        internal static void Flush()
        {
            glFlush();
            CheckErrors();
        }

        #endregion

        #region Gen Buffers

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

        #endregion

        #region Gen Textures

        delegate void GenTexturesDelegate(int n, ref uint textures);
        static readonly GenTexturesDelegate glGenTextures;

        internal static void GenTextures(int n, ref uint textures)
        {
            glGenTextures(n, ref textures);
            CheckErrors();
        }

        #endregion

        #region Gen Vertex Arrays

        delegate void GenVertexArraysDelegate(int n, ref uint arrays);
        static readonly GenVertexArraysDelegate glGenVertexArrays;

        internal static void GenVertexArrays(int n, ref uint arrays)
        {
            glGenVertexArrays(n, ref arrays);
            CheckErrors();
        }

        #endregion

        #region Get Program Info Log

        delegate void GetProgramInfoLogDelegate(uint program, int maxLength, out int length, [Out] byte[] infoLog);
        static readonly GetProgramInfoLogDelegate glGetProgramInfoLog;

        internal static void GetProgramInfoLog(uint program, int maxLength, out int length, byte[] infoLog)
        {
            glGetProgramInfoLog(program, maxLength, out length, infoLog);
            CheckErrors();
        }

        #endregion

        #region Get Program Integer Vector

        delegate void GetProgramivDelegate(uint program, uint pname, out bool success);
        static readonly GetProgramivDelegate glGetProgramiv;

        internal static void GetProgramiv(uint program, ParameterName pname, out bool success)
        {
            glGetProgramiv(program, (uint)pname, out success);
            CheckErrors();
        }

        #endregion

        #region Get Shader Info Log

        delegate void GetShaderInfoLogDelegate(uint shader, int maxLength, out int length, char[] infoLog);
        static readonly GetShaderInfoLogDelegate glGetShaderInfoLog;

        internal static void GetShaderInfoLog(uint shader, int maxLength, out int length, char[] infoLog)
        {
            glGetShaderInfoLog(shader, maxLength, out length, infoLog);
            CheckErrors();
        }

        #endregion

        #region Get Shader Integer Vector

        delegate void GetShaderivDelegate(uint shader, uint pname, out bool success);
        static readonly GetShaderivDelegate glGetShaderiv;

        internal static void GetShaderiv(uint shader, ParameterName pname, out bool success)
        {
            glGetShaderiv(shader, (uint)pname, out success);
            CheckErrors();
        }

        #endregion

        #region Get Uniform Location

        delegate int GetUniformLocationDelegate(uint program, string name);
        static readonly GetUniformLocationDelegate glGetUniformLocation;

        internal static int GetUniformLocation(uint program, string name)
        {
            var location = glGetUniformLocation(program, name);
            CheckErrors();
            return location;
        }

        #endregion

        #region Link Program

        delegate void LinkProgramDelegate(uint program);
        static readonly LinkProgramDelegate glLinkProgram;

        internal static void LinkProgram(uint program)
        {
            glLinkProgram(program);
            CheckErrors();
        }

        #endregion

        #region Shader Source  

        delegate void ShaderSourceDelegate(uint shader, int count, string[] source, int[] length);
        static readonly ShaderSourceDelegate glShaderSource;

        internal static void ShaderSource(uint shader, int count, string[] source, int[] length)
        {
            glShaderSource(shader, count, source, length);
            CheckErrors();
        }

        #endregion

        #region Texture Image 2D

        delegate void TexImage2DDelegate(uint target, int level, int internalFormat, int width, int height, int border, uint format, uint type, float[] pixels);
        static readonly TexImage2DDelegate glTexImage2D;

        internal static void TexImage2D(TextureTarget target, int level, int internalFormat, int width, int height, int border, uint format, uint type, float[] pixels)
        {
            glTexImage2D((uint)target, level, internalFormat, width, height, border, format, type, pixels);
            CheckErrors();
        }

        #endregion

        #region Texture Image 3D

        delegate void TexImage3DDelegate(uint target, int level, int internalFormat, int width, int height, int depth, int border, uint format, uint type, float[] pixels);
        static readonly TexImage3DDelegate glTexImage3D;

        internal static void TexImage3D(TextureTarget target, int level, int internalFormat, int width, int height, int depth, int border, uint format, uint type, float[] pixels)
        {
            glTexImage3D((uint)target, level, internalFormat, width, height, depth, border, format, type, pixels);
            CheckErrors();
        }

        #endregion

        #region Texture Parameter Float

        delegate void TexParameterfDelegate(uint target, uint pname, float param);
        static readonly TexParameterfDelegate glTexParameterf;

        internal static void TexParameterf(TextureTarget target, TextureParameterName pname, float param)
        {
            glTexParameterf((uint)target, (uint)pname, param);
            CheckErrors();
        }

        #endregion

        #region Texture Parameter Float Vector

        delegate void TexParameterfvDelegate(uint target, uint pname, float[] param);
        static readonly TexParameterfvDelegate glTexParameterfv;

        internal static void TexParameterfv(TextureTarget target, TextureParameterName pname, float[] param)
        {
            glTexParameterfv((uint)target, (uint)pname, param);
            CheckErrors();
        }

        #endregion

        #region Texture Parameter Integer

        delegate void TexParameteriDelegate(uint target, uint pname, int param);
        static readonly TexParameteriDelegate glTexParameteri;

        internal static void TexParameteri(TextureTarget target, TextureParameterName pname, int param)
        {
            glTexParameteri((uint)target, (uint)pname, param);
            CheckErrors();
        }

        #endregion

        #region Texture Sub Image 2D

        delegate void TexSubImage2DDelegate(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, float[] pixels);
        static readonly TexSubImage2DDelegate glTexSubImage2D;

        internal static void TexSubImage2D(TextureTarget target, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, float[] pixels)
        {
            glTexSubImage2D((uint)target, level, xoffset, yoffset, width, height, format, type, pixels);
            CheckErrors();
        }

        #endregion

        #region Use Program

        delegate void UseProgramDelegate(uint program);
        static readonly UseProgramDelegate glUseProgram;

        internal static void UseProgram(uint program)
        {
            glUseProgram(program);
            CheckErrors();
        }

        #endregion

        #region Vertex Attribute Pointer

        delegate void VertexAttribPointerDelegate(uint index, int size, uint type, bool normalized, int stride, int offset);
        static readonly VertexAttribPointerDelegate glVertexAttribPointer;

        internal static void VertexAttribPointer(uint index, int size, DataType type, bool normalized, int stride, int offset)
        {
            glVertexAttribPointer(index, size, (uint)type, normalized, stride, offset);
            CheckErrors();
        }

        #endregion

        #region Viewport

        delegate void ViewportDelegate(int x, int y, int width, int height);
        static readonly ViewportDelegate glViewport;

        internal static void Viewport(int x, int y, int width, int height)
        {
            glViewport(x, y, width, height);
            CheckErrors();
        }

        #endregion

        #region Uniform 4 Float

        delegate void Uniform4fDelegate(int location, float v0, float v1, float v2, float v3);
        static readonly Uniform4fDelegate glUniform4f;

        internal static void Uniform4f(int location, float v0, float v1, float v2, float v3)
        {
            glUniform4f(location, v0, v1, v2, v3);
            CheckErrors();
        }

        #endregion

        #region Check Errors

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

        #endregion
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

internal enum TextureTarget : uint
{
    Texture1D = 0x0DE0,
    Texture2D = 0x0DE1,
    Texture3D = 0x806F,
    TextureCubeMap = 0x8513
}

internal enum TextureParameterName : uint
{
    TextureMinFilter = 0x2801,
    TextureMagFilter = 0x2800,
    TextureWrapS = 0x2802,
    TextureWrapT = 0x2803,
    TextureWrapR = 0x8072
}

internal enum TextureMinFilter : uint
{
    Nearest = 0x2600,
    Linear = 0x2601,
    NearestMipmapNearest = 0x2700,
    LinearMipmapNearest = 0x2701,
    NearestMipmapLinear = 0x2702,
    LinearMipmapLinear = 0x2703
}

internal enum TextureMagFilter : uint
{
    Nearest = 0x2600,
    Linear = 0x2601
}

internal enum TextureWrapMode : uint
{
    ClampToEdge = 0x812F,
    ClampToBorder = 0x812D,
    MirroredRepeat = 0x8370,
    Repeat = 0x2901
}

internal enum TextureUnit : uint
{
    Texture0 = 0x84C0,
    Texture1 = 0x84C1,
    Texture2 = 0x84C2,
    Texture3 = 0x84C3,
    Texture4 = 0x84C4,
    Texture5 = 0x84C5,
    Texture6 = 0x84C6,
    Texture7 = 0x84C7,
    Texture8 = 0x84C8,
    Texture9 = 0x84C9,
    Texture10 = 0x84CA,
    Texture11 = 0x84CB,
    Texture12 = 0x84CC,
    Texture13 = 0x84CD,
    Texture14 = 0x84CE,
    Texture15 = 0x84CF,
    Texture16 = 0x84D0,
    Texture17 = 0x84D1,
    Texture18 = 0x84D2,
    Texture19 = 0x84D3,
    Texture20 = 0x84D4,
    Texture21 = 0x84D5,
    Texture22 = 0x84D6,
    Texture23 = 0x84D7,
    Texture24 = 0x84D8,
    Texture25 = 0x84D9,
    Texture26 = 0x84DA,
    Texture27 = 0x84DB,
    Texture28 = 0x84DC,
    Texture29 = 0x84DD,
    Texture30 = 0x84DE,
    Texture31 = 0x84DF
}