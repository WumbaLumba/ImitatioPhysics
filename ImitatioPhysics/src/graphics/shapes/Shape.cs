using OpenTK.Mathematics;

namespace ImitatioPhysics
{
    // Maybe add texture coordinates and colour?
    struct Vertex
    {
        public Vector3 Position;
    }

    // Does it even need a base class?
    // All code associated with vertices 'n' stuff from ImitatioWindow goes here:
    class Shape
    {
        private VertexArray _vertexArray;
        private VertexBuffer _vertexBuffer;
        public Shape()
        {
            _vertexArray = new VertexArray();
            // Not sure if this will work.
            unsafe
            {
                _vertexBuffer = new VertexBuffer(new float[0], sizeof(Vertex) * 1000);
            }
        }  
    }
}
