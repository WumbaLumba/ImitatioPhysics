using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace ImitatioPhysics
{
    class Shader
    {
        // shader handle
        private int _rendererID;
        private string _vertexPath, _fragmentPath; // for debugging

        // Create program with specified source code for shaders.
        public Shader(string vertexPath, string fragmentPath)
        {
            _vertexPath = vertexPath;
            _fragmentPath = fragmentPath;

            // Read source code file and store it as string.

            string vertexShaderSource;
            using (StreamReader reader = new StreamReader(vertexPath))
            {
                vertexShaderSource = reader.ReadToEnd();
            }

            string fragmentShaderSource;
            using (StreamReader reader = new StreamReader(fragmentPath))
            {
                fragmentShaderSource = reader.ReadToEnd();
            }

            _rendererID = CreateShader(vertexShaderSource, fragmentShaderSource);
        }

        // Compile source code and return ID
        private int CompileShader(ShaderType type, string source)
        {
            // shader handle
            int shaderID = GL.CreateShader(type);

            GL.ShaderSource(shaderID, source);
            GL.CompileShader(shaderID);

            // Check for errors in source code.
            string infoLogShader = GL.GetShaderInfoLog(shaderID);
            if (infoLogShader != string.Empty)
                Console.WriteLine(infoLogShader);

            return shaderID;
        }

        private int CreateShader(string vertexShader, string fragmentShader)
        {
            // program handle
            int program = GL.CreateProgram();

            // vertex shader handle
            int vs = CompileShader(ShaderType.VertexShader, vertexShader);

            // fragment shader handle
            int fs = CompileShader(ShaderType.FragmentShader, fragmentShader);

            GL.AttachShader(program, vs);
            GL.AttachShader(program, fs);
            GL.LinkProgram(program);
            GL.ValidateProgram(program);

            GL.DeleteShader(vs);
            GL.DeleteShader(fs);

            return program;
        }

        public void Bind()
        {
            GL.UseProgram(_rendererID);
        }

        public void Unbind()
        {
            GL.UseProgram(0);
        }

        public void SetUniform1(string name, double value)
        {
            GL.Uniform1(GetUniformLocation(name), value);
        }

        public void SetUniform4(string name, float f0, float f1, float f2, float f3)
        {
            GL.Uniform4(GetUniformLocation(name), f0, f1, f2, f3);
        }

        public void SetUniformMat4(string name, ref Matrix4 mat4)
        {
            GL.UniformMatrix4(GetUniformLocation(name), false, ref mat4);
        }

        private int GetUniformLocation(string name)
        {
            int location = GL.GetUniformLocation(_rendererID, name);
            
            // Print error if uniform doesn't exist in shader.
            if (location == -1)
                Console.WriteLine($"Uniform {name} does not exist.");

            return location;
        }

        ~Shader()
        {
            GL.DeleteProgram(_rendererID);
        }
    }
}

