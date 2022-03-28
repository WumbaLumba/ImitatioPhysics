using OpenTK.Mathematics;
using System.Collections.Generic;

namespace ImitatioPhysics
{
    class Shape
    {
        protected Vector4 _color;

        protected VertexBuffer _vertexBuffer;
        protected IndexBuffer _indexBuffer;
        protected VertexArray _vertexArray;
        protected VertexBufferLayout _layout;

        protected Shader _shader;
        protected Renderer _renderer = new Renderer();

        protected List<float> _positionsShape = new List<float>();
        protected List<uint> _indicesShape = new List<uint>();

        // Default translation/position for object to (0, 0).
        protected static Particle _particle = new Particle(new Vector3(0.0f, 0.0f, 0.0f));

        // Camera funcitonality to be implemented.
        protected static Matrix4 _view = Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);

        // Shape's appearence and movement is controlled with this.
        protected static Matrix4 _model = Matrix4.CreateTranslation(_particle.Position);

        // Adjust scale of shape for window size.
        protected static Matrix4 _proj =
            Matrix4.CreateOrthographicOffCenter(0.0f, 960.0f,    // left -> right
                                                0.0f, 540.0f,    // bottom -> top
                                               -1.0f, 1.0f);     // far -> near

        // Final matrix used to apply all transformations (model, view, projection).
        protected static Matrix4 _mvp;
     
        public Shape()
        {
            // Default to white.
            _color = (1.0f, 1.0f, 1.0f, 1.0f);

            _vertexArray = new VertexArray();
            _vertexBuffer = new VertexBuffer(GenerateBuffer(_positionsShape), _positionsShape.Count * sizeof(float));
            _layout = new VertexBufferLayout();

            // Add 2D positions to layout.
            _layout.PushFloat(2);

            _vertexArray.AddBuffer(_vertexBuffer, _layout);
            _indexBuffer = new IndexBuffer(GenerateBuffer(_indicesShape), _indicesShape.Count);

            _shader = new Shader("res/shaders/shader.vert", "res/shaders/shader.frag");
            _shader.Bind();
            _shader.SetUniformMat4("u_MVP", ref _mvp);

            _vertexArray.Unbind();
            _shader.Unbind();
            _vertexBuffer.UnBind();
            _indexBuffer.UnBind();
        }

        // Generic method for converting lists into buffers (arrays).
        private T[] GenerateBuffer<T>(List<T> list)
        {
            T[] buffer = new T[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                buffer[i] = list[i];
            }

            // Return array of specified T type.
            return buffer;
        }

        public void Render()
        {
            _shader.Bind();

            _model = Matrix4.CreateTranslation(_particle.Position);

            _mvp = _model * _view * _proj;

            _shader.SetUniformMat4("u_MVP", ref _mvp);
            _shader.SetUniform4("u_Color", _color.X, _color.Y, _color.Z, _color.W);

            _vertexArray.Bind();
            _indexBuffer.Bind();

            // Draw object.
            _renderer.Draw(ref _vertexArray, ref _indexBuffer, ref _shader);
        }

        public void ChangeColor(Vector4 color)
        {
            _color = color;
        }

        public void Move(float dt)
        {
            _particle.Update(dt);
        }

        public Vector3 GetVelocity() => _particle.Velocity;

        public Vector3 GetPosition() => _particle.Position;

        public void SetVelocity(Vector3 vel) => _particle.Velocity = (vel.X, vel.Y, vel.Z);
            
        public void SetPosition(Vector3 pos) => _particle.Position = (pos.X, pos.Y, pos.Z);
    }
}