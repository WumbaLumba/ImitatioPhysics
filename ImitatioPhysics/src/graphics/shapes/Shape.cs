using OpenTK.Mathematics;

namespace ImitatioPhysics
{
    class Shape
    {
        protected Vector4 _color;

        protected static Vector3 _trans = (0.0f, 0.0f, 0.0f);

        protected VertexBuffer _vertexBuffer;
        protected IndexBuffer _indexBuffer;
        protected VertexArray _vertexArray;
        protected VertexBufferLayout _layout;

        protected Shader _shader;
        protected Renderer _renderer = new Renderer();

        protected static Matrix4 _view =  Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);
        protected static Matrix4 _model = Matrix4.CreateTranslation(_trans);
        protected static Matrix4 _mvp;

        protected static Matrix4 _proj = Matrix4.CreateOrthographicOffCenter(0.0f, 960.0f,    // left -> right
                                                                             0.0f, 540.0f,    // bottom -> top
                                                                            -1.0f,   1.0f);     // far -> near

        // Default translation/position for object is 0, 0
        protected Particle _particle = new Particle(new Vector3(0.0f, 0.0f, 0.0f));

        public Shape()
        {
            _color = (1.0f, 1.0f, 1.0f, 1.0f);

        }

        public void Render()
        {
            _shader.Bind();

            _model = Matrix4.CreateTranslation(_trans);

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

        public void Move(Vector3 vec)
        {
            _trans = vec;

        }
    }
}
