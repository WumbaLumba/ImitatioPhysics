using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace ImitatioPhysics
{
    class Shader
    {
        private int _renderedID;
        private string _vertexPath, _fragmentPath;

        public Shader(string vertexPath, string fragmentPath)
        {
            _vertexPath = vertexPath;
            _fragmentPath = fragmentPath;

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

            _renderedID = CreateShader(vertexShaderSource, fragmentShaderSource);
        }

        // Compile source code and return ID
        private int CompileShader(ShaderType type, string source)
        {
            int id = GL.CreateShader(type);
            GL.ShaderSource(id, source);
            GL.CompileShader(id);

            string infoLogVert = GL.GetShaderInfoLog(id);
            if (infoLogVert != System.String.Empty)
                Console.WriteLine(infoLogVert);

            return id;
        }

        private int CreateShader(string vertexShader, string fragmentShader)
        {
            int program = GL.CreateProgram();
            int vs = CompileShader(ShaderType.VertexShader, vertexShader);
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
            GL.UseProgram(_renderedID);
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
            int location = GL.GetUniformLocation(_renderedID, name);
            if (location == -1)
                Console.WriteLine($"Uniform {name} does not exist.");
            return location;
        }

        ~Shader()
        {
            GL.DeleteProgram(_renderedID);
        }
    }
}

