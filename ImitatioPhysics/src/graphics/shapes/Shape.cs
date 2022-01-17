using OpenTK.Mathematics;
using System.Collections.Generic;

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

        private List<Vertex> _vertices = new List<Vertex>();
        private float[] _verticesF = new float[1000];

        public Shape()
        {
            // Some way of adding data
            /*foreach (Vertex element in _vertices)
            {
                for 
            }*/

            _vertexArray = new VertexArray();
            // Not sure if this will work.
            unsafe
            {
                _vertexBuffer = new VertexBuffer(new float[0], sizeof(Vertex) * 1000);
            }
        }  
    }
}
