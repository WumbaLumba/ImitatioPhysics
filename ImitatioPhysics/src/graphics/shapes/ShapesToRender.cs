using OpenTK.Mathematics;

namespace ImitatioPhysics
{
    class ShapesToRender
    {
        protected Vector4 _color;

        protected VertexBuffer _vertexBuffer;
        protected IndexBuffer _indexBuffer;
        protected VertexArray _vertexArray;
        protected VertexBufferLayout _layout;

        protected Shader _shader;
        protected Renderer _renderer = new Renderer();


        // Camera functionality to be implemented.
        protected static Matrix4 _view = Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);

        // Shape's appearence and movement is controlled with this.
        protected static Matrix4 _model = Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);

        // Adjust scale of shape for window size.
        protected static Matrix4 _proj =
            Matrix4.CreateOrthographicOffCenter(0.0f, 960.0f,    // left -> right
                                                0.0f, 540.0f,    // bottom -> top
                                               -1.0f, 1.0f);     // far -> near

        // Final matrix used to apply all transformations (model, view, projection).
        protected static Matrix4 _mvp = _model * _view * _proj;

        private static int _maxQuadCount = 1000;
        private static int _maxVertexCount = _maxQuadCount * 4;
        private static int _maxIndexCount = _maxQuadCount * 6;

        private uint[] _indices = new uint[_maxIndexCount];

        List <Square> _squares = new List <Square>();
        private List<float> _verticesList = new List<float>();
        private float[] _vertices = new float[_maxVertexCount];
        public ShapesToRender()
        {
            uint offset = 0;
            for (int i = 0; i < _maxIndexCount; i += 6)
            {
                _indices[i + 0] = 0 + offset;
                _indices[i + 1] = 1 + offset;
                _indices[i + 2] = 2 + offset;
                _indices[i + 3] = 2 + offset;
                _indices[i + 4] = 3 + offset;
                _indices[i + 5] = 0 + offset;

                offset += 4;
            }

            _vertexArray = new VertexArray();

            // 2 position coord 4 color coord
            _vertexBuffer = new VertexBuffer(6 * _maxVertexCount * sizeof(float));
            _layout = new VertexBufferLayout();

            // Add 2D positions to layout.
            _layout.PushFloat(2);

            // Add colours to layout.
            _layout.PushFloat(4);

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

        public void AddShape(Square square)
        {
            _squares.Add(square);
            _verticesList.AddRange(square.GetVertices());
            _vertices = GenerateBuffer(_verticesList);
        }

        public void Render()
        {
            for(int i = 0; i < _squares.Count; i++)
            {
                _squares[i].UpdateVertices();
            }

            _vertexBuffer.Bind();
            _vertexBuffer.UpdateData(_vertices);

            _shader.Bind();

            _mvp = _model * _view * _proj;

            _shader.SetUniformMat4("u_MVP", ref _mvp);

            _vertexArray.Bind();

            _indexBuffer.Bind();

            // Draw object.
            _renderer.Draw(ref _vertexArray, ref _indexBuffer, ref _shader);

            _vertexArray.Unbind();
            _indexBuffer.UnBind();
            _shader.Unbind();
        }

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

        public List<Square> GetListSquares() => _squares;
    }
}