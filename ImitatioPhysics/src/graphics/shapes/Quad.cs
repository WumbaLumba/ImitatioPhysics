using OpenTK.Mathematics;

namespace ImitatioPhysics
{
    class Quad : Shape
    {
        protected float[] _positions = new float[]
        {
               0.0f,   0.0f,  // 0 bottom-left
             100.0f,   0.0f,  // 1 bottom-right
             100.0f, 100.0f,  // 2 top-right
               0.0f, 100.0f,  // 3 top-left
        };

        protected uint[] _indices = new uint[]
        {
            0, 1, 2,
            2, 3, 0
        };

        public Quad() : base()
        {
            _vertexArray = new VertexArray();
            _vertexBuffer = new VertexBuffer(_positions, _positions.Length * sizeof(float));

            _layout = new VertexBufferLayout();

            // Add 2D positions to layout.
            _layout.PushFloat(2);

            _vertexArray.AddBuffer(_vertexBuffer, _layout);

            _indexBuffer = new IndexBuffer(_indices, _indices.Length);

            _shader = new Shader("res/shaders/shader.vert", "res/shaders/shader.frag");
            _shader.Bind();
            _shader.SetUniformMat4("u_MVP", ref _mvp);

            _vertexArray.Unbind();
            _shader.Unbind();
            _vertexBuffer.UnBind();
            _indexBuffer.UnBind();
        }
    }
}
