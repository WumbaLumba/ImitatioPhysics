using OpenTK.Mathematics;

namespace ImitatioPhysics
{
    struct Vertex
    {
        public Vector3 Position;
    }

    class Shape
    {
        private VertexArray _vertexArray;
        private VertexBuffer _vertexBuffer;
        public Shape()
        {
            _vertexArray = new VertexArray();
            unsafe
            {
                _vertexBuffer = new VertexBuffer(new float[0], sizeof(Vertex) * 1000);
            }
        }  
    }
}
