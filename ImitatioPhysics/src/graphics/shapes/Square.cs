using OpenTK.Mathematics;

namespace ImitatioPhysics
{
    struct Vertex
    {
        public float[] Position = new float[2];
        public float[] Color = new float[4];

        public Vertex()
        { }
    }
    class Square
    {
        /*
        protected float[] _positions = new float[]
        {
               0.0f,   0.0f, 0.18f, 0.6f, 0.96f, 1.0f,   // 0 bottom-left
             100.0f,   0.0f, 0.18f, 0.6f, 0.96f, 1.0f,  // 1 bottom-right
             100.0f, 100.0f, 0.18f, 0.6f, 0.96f, 1.0f,  // 2 top-right
               0.0f, 100.0f, 0.18f, 0.6f, 0.96f, 1.0f,   // 3 top-left

             200.0f,   0.0f, 1.0f, 0.93f, 0.24f, 1.0f,//4
             300.0f,   0.0f, 1.0f, 0.93f, 0.24f, 1.0f,//5
             300.0f, 100.0f, 1.0f, 0.93f, 0.24f, 1.0f,//6
             200.0f, 100.0f, 1.0f, 0.93f, 0.24f, 1.0f //7
        };
        */
        
       //protected uint[] _indices = new uint[]
       //{
       //    0, 1, 2,
       //    2, 3, 0,
       //
       //    4, 5, 6,
       //    6, 7, 4
       //};

        protected Vector4 _color;

        protected VertexBuffer _vertexBuffer;
        protected IndexBuffer _indexBuffer;
        protected VertexArray _vertexArray;
        protected VertexBufferLayout _layout;

        protected Shader _shader;
        protected Renderer _renderer = new Renderer();

        //protected List<float> _positionsShape = new List<float>();
        //protected List<uint> _indicesShape = new List<uint>();

        // Default translation/position for object to (0, 0).
        protected static Particle _particle = new Particle(new Vector3(0.0f, 0.0f, 0.0f));

        // Camera functionality to be implemented.
        protected static Matrix4 _view = Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);

        // Shape's appearence and movement is controlled with this.
        protected static Matrix4 _model = Matrix4.CreateTranslation(_particle.Position);

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

        public Square()
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
        public float[] CreateQuad(float x, float y)
        {
            int size = 100;

            float[] vertices = new float[] {
                x,        y,        0.18f, 0.6f, 0.96f, 1.0f,
                x + size, y,        0.18f, 0.6f, 0.96f, 1.0f,
                x + size, y + size, 0.18f, 0.6f, 0.96f, 1.0f,
                x,        y + size, 0.18f, 0.6f, 0.96f, 1.0f
            };

            return vertices;
        }

        public void Render()
        {
            float[] q1 = CreateQuad(0.0f, 0.0f);
            float[] q2 = CreateQuad(200.0f, 0.0f);

            List<float> vertices = new List<float>();
            vertices.AddRange(q1);
            vertices.AddRange(q2);

            _vertexBuffer.Bind();
            _vertexBuffer.UpdateData(GenerateBuffer(vertices));

            _shader.Bind();

            _model = Matrix4.CreateTranslation(_particle.Position);

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

        public void ChangeColor(Vector4 color)
        {
            _color = color;
        }

        public void Move(float dt)
        {
            _particle.Update(dt);
            _model = Matrix4.CreateTranslation(_particle.Position);
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

        public Vector3 GetVelocity() => _particle.Velocity;

        public Vector3 GetPosition() => _particle.Position;

        public void SetVelocity(Vector3 vel) => _particle.Velocity = (vel.X, vel.Y, vel.Z);

        public void SetPosition(Vector3 pos) => _particle.Position = (pos.X, pos.Y, pos.Z);

        public float GetMass() => _particle.GetMass();
    }
}
