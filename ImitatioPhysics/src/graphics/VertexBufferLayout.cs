using OpenTK.Graphics.OpenGL;

namespace ImitatioPhysics
{
    // one attribute of the vertex buffer
    struct VertexBufferElement
    {
        // data type provided
        // refers to OpenGl data types
        public VertexAttribPointerType Type;
        
        // no. floats available for one element
        public int Count;
        
        // if false, OpenGL will normalise it
        public bool Normalized;

        public VertexBufferElement(VertexAttribPointerType type, int count, bool normalized)
        {
            Count = count;
            Type = type;
            Normalized = normalized;
        }

        // Calculate size of OpenGL data type in bytes
        public static int GetSizeOfType(VertexAttribPointerType type)
        {
            // Add values for other data types here:
            switch (type)
            {
                case VertexAttribPointerType.Float:         
                    return 4;
            }

            return 0;
        }
    }

    class VertexBufferLayout
    {
        private List<VertexBufferElement> _elements = new List<VertexBufferElement>();
        
        // amount of bytes between each vertex
        private int _stride;

        public VertexBufferLayout() 
        { 
        }

        // Add methods for pushing other data types here: 
        public void PushFloat(int count)
        {
            _elements.Add(new VertexBufferElement(VertexAttribPointerType.Float, count, false));
            _stride += count * VertexBufferElement.GetSizeOfType(VertexAttribPointerType.Float);
        }


        public int GetStride()
        {
            return _stride;
        }

        public List<VertexBufferElement> GetElements()
        {
            return _elements;
        }
    }
}
