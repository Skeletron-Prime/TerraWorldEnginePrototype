using System.Diagnostics;
using System.Runtime.InteropServices;
using TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL.Primitives;

namespace TerraWorldEnginePrototype.PlatformIndependence.Rendering.OpenGL
{
    /// <summary>
    /// Represents the OpenGL API. This class is a wrapper around the OpenGL API functions. Contains all OpenGL functions, that are used in the project.
    /// </summary>
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
            LoadFunction(out glBufferSubData, nameof(glBufferSubData));
            LoadFunction(out glActiveTexture, nameof(glActiveTexture));
            LoadFunction(out glAttachShader, nameof(glAttachShader));
            LoadFunction(out glBindVertexArray, nameof(glBindVertexArray));
            LoadFunction(out glBufferData, nameof(glBufferData));
            LoadFunction(out glCompileShader, nameof(glCompileShader));
            LoadFunction(out glCreateProgram, nameof(glCreateProgram));
            LoadFunction(out glCreateShader, nameof(glCreateShader));
            LoadFunction(out glCullFace, nameof(glCullFace));
            LoadFunction(out glDeleteBuffers, nameof(glDeleteBuffers));
            LoadFunction(out glDeleteProgram, nameof(glDeleteProgram));
            LoadFunction(out glDeleteShader, nameof(glDeleteShader));
            LoadFunction(out glDeleteTextures, nameof(glDeleteTextures));
            LoadFunction(out glDeleteVertexArrays, nameof(glDeleteVertexArrays));
            LoadFunction(out glDrawArrays, nameof(glDrawArrays));
            LoadFunction(out glDrawElements, nameof(glDrawElements));
            LoadFunction(out glEnableVertexAttribArray, nameof(glEnableVertexAttribArray));
            LoadFunction(out glFlush, nameof(glFlush));
            LoadFunction(out glGenBuffers, nameof(glGenBuffers));
            LoadFunction(out glGenerateMipmap, nameof(glGenerateMipmap));
            LoadFunction(out glGenTextures, nameof(glGenTextures));
            LoadFunction(out glGenVertexArrays, nameof(glGenVertexArrays));
            LoadFunction(out glGetAttribLocation, nameof(glGetAttribLocation));
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

        #region Active Texture

        delegate void ActiveTextureDelegate(uint texture);
        static readonly ActiveTextureDelegate glActiveTexture;

        internal static void ActiveTexture(TextureUnit texture)
        {
            glActiveTexture((uint)texture);
            CheckErrors();
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

        delegate void BindBufferDelegate(uint target, uint buffer);
        static readonly BindBufferDelegate glBindBuffer;

        /// <summary>
        /// Binds a buffer object to a buffer target. What this means is that
        /// the buffer object will now be used whenever some operation requires
        /// a buffer of the specified target.
        /// </summary>
        /// <param name="target">The target to which the buffer object is bound</param>
        /// <param name="buffer">The buffer object to bind</param>
        internal static void BindBuffer(BufferType target, uint buffer)
        {
            glBindBuffer((uint)target, buffer);
            CheckErrors();
        }

        #endregion

        #region Bind Texture

        delegate void BindTextureDelegate(uint target, uint texture);
        static readonly BindTextureDelegate glBindTexture;

        internal static void BindTexture(TextureType target, uint texture)
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

        unsafe delegate void BufferDataDelegate(uint target, int size, void* data, uint usage);
        static readonly BufferDataDelegate glBufferData;

        public unsafe static void BufferData<T>(BufferType target, T[] data, BufferUsage usage) where T : unmanaged
        {
            var size = data.Length * sizeof(T);

            fixed (T* dataPtr = data)
            {
                glBufferData((uint)target, size, dataPtr, (uint)usage);
            }
            CheckErrors();
        }

        #endregion

        #region Buffer Sub Data

        unsafe delegate void BufferSubDataDelegate(uint target, int offset, int size, void* data);
        static readonly BufferSubDataDelegate glBufferSubData;

        public unsafe static void BufferSubData<T>(BufferType target, int offset, T[] data) where T : unmanaged
        {
            var size = data.Length * sizeof(T);

            fixed (T* dataPtr = data)
            {
                glBufferSubData((uint)target, offset, size, dataPtr);
            }
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

        #region Delete Buffer

        delegate void DeleteBuffersDelegate(int n, ref uint buffers);
        static readonly DeleteBuffersDelegate glDeleteBuffers;

        public static void DeleteBuffer(uint buffer)
        {
            glDeleteBuffers(1, ref buffer);
            CheckErrors();
        }

        #endregion

        #region Delete Program

        delegate void DeleteProgramDelegate(uint program);
        static readonly DeleteProgramDelegate glDeleteProgram;

        internal static void DeleteProgram(uint program)
        {
            glDeleteProgram(program);
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

        internal static void DeleteTexture(uint texture)
        {
            glDeleteTextures(1, ref texture);
            CheckErrors();
        }

        #endregion

        #region Delete Vertex Array

        delegate void DeleteVertexArraysDelegate(int n, ref uint arrays);
        static readonly DeleteVertexArraysDelegate glDeleteVertexArrays;

        public static void DeleteVertexArray(uint array)
        {
            glDeleteVertexArrays(1, ref array);
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

        public static void DrawElements(DrawMode mode, int count, DataType type, int indices)
        {
            glDrawElements((uint)mode, count, (uint)type, indices);
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

        #region Gen Buffer

        delegate void GenBuffersDelegate(int n, ref uint buffers);
        static readonly GenBuffersDelegate glGenBuffers;

        /// <summary>
        /// Generates buffer object name. This function in itself does not
        /// create any buffer but rather helps to find an unused name for a new buffer object.
        /// </summary>
        /// <returns>The name of the buffer object</returns>
        public static uint GenBuffer()
        {
            uint buffer = 0;
            glGenBuffers(1, ref buffer);
            CheckErrors();
            return buffer;
        }

        #endregion

        #region Gen Mipmap

        delegate void GenerateMipmapDelegate(uint target);
        static readonly GenerateMipmapDelegate glGenerateMipmap;

        internal static void GenerateMipmap(TextureType target)
        {
            glGenerateMipmap((uint)target);
            CheckErrors();
        }

        #endregion

        #region Gen Texture

        delegate void GenTexturesDelegate(int n, ref uint textures);
        static readonly GenTexturesDelegate glGenTextures;

        internal static uint GenTexture()
        {
            uint texture = 0;
            glGenTextures(1, ref texture);
            CheckErrors();
            return texture;
        }

        #endregion

        #region Gen Vertex Array

        delegate void GenVertexArraysDelegate(int n, ref uint arrays);
        static readonly GenVertexArraysDelegate glGenVertexArrays;

        public static uint GenVertexArray()
        {
            uint array = 0;
            glGenVertexArrays(1, ref array);
            CheckErrors();
            return array;
        }

        #endregion

        #region Get Attribute Location

        delegate int GetAttribLocationDelegate(uint program, string name);
        static readonly GetAttribLocationDelegate glGetAttribLocation;

        internal static int GetAttribLocation(uint program, string name)
        {
            var location = glGetAttribLocation(program, name);
            CheckErrors();
            return location;
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

        internal static void GetProgramiv(uint program, ShaderParameterName pname, out bool success)
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

        internal static void GetShaderiv(uint shader, ShaderParameterName pname, out bool success)
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

        internal static void ShaderSource(uint shader, string source)
        {
            glShaderSource(shader, 1, [source], [source.Length]);
            CheckErrors();
        }

        #endregion

        #region Texture Image 2D

        unsafe delegate void TexImage2DDelegate(uint target, int level, int internalFormat, int width, int height, int border, uint format, uint type, void* pixels);
        static readonly TexImage2DDelegate glTexImage2D;

        internal unsafe static void TexImage2D<T>(TextureType target, int level, PixelInternalFormat internalFormat, int width, int height, int border, PixelFormat format, PixelType type, T[] pixels) where T : unmanaged
        {
            fixed (T* pixelsPtr = pixels)
            {
                glTexImage2D((uint)target, level, (int)internalFormat, width, height, border, (uint)format, (uint)type, pixelsPtr);
            }
            CheckErrors();
        }

        #endregion

        #region Texture Image 3D

        delegate void TexImage3DDelegate(uint target, int level, int internalFormat, int width, int height, int depth, int border, uint format, uint type, float[] pixels);
        static readonly TexImage3DDelegate glTexImage3D;

        internal static void TexImage3D(TextureType target, int level, int internalFormat, int width, int height, int depth, int border, uint format, uint type, float[] pixels)
        {
            glTexImage3D((uint)target, level, internalFormat, width, height, depth, border, format, type, pixels);
            CheckErrors();
        }

        #endregion

        #region Texture Parameter Float

        delegate void TexParameterfDelegate(uint target, uint pname, float param);
        static readonly TexParameterfDelegate glTexParameterf;

        internal static void TexParameter(TextureType target, TextureParameterName pname, float param)
        {
            glTexParameterf((uint)target, (uint)pname, param);
            CheckErrors();
        }

        #endregion

        #region Texture Parameter Float Vector

        delegate void TexParameterfvDelegate(uint target, uint pname, float[] param);
        static readonly TexParameterfvDelegate glTexParameterfv;

        internal static void TexParameter(TextureType target, TextureParameterName pname, float[] param)
        {
            glTexParameterfv((uint)target, (uint)pname, param);
            CheckErrors();
        }

        #endregion

        #region Texture Parameter Integer

        delegate void TexParameteriDelegate(uint target, uint pname, int param);
        static readonly TexParameteriDelegate glTexParameteri;

        internal static void TexParameter(TextureType target, TextureParameterName pname, int param)
        {
            glTexParameteri((uint)target, (uint)pname, param);
            CheckErrors();
        }

        #endregion

        #region Texture Sub Image 2D

        delegate void TexSubImage2DDelegate(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, float[] pixels);
        static readonly TexSubImage2DDelegate glTexSubImage2D;

        internal static void TexSubImage2D(TextureType target, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, float[] pixels)
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