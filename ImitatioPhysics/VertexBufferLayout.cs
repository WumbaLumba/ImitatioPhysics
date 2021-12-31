using OpenTK.Graphics.OpenGL;

namespace ImitatioPhysics
{
    struct VertexBufferElement
    {
        public VertexAttribPointerType Type;
        public int Count;
        public bool Normalized;

        public VertexBufferElement(VertexAttribPointerType type, int count, bool normalized)
        {
            Count = count;
            Type = type;
            Normalized = normalized;
        }

        public static int GetSizeOfType(VertexAttribPointerType type)
        {
            switch (type)
            {
                case VertexAttribPointerType.Float:         return 4;
                case VertexAttribPointerType.UnsignedInt:   return 4;
                case VertexAttribPointerType.UnsignedByte:  return 1;
            }

            return 0;
        }
    }

    class VertexBufferLayout
    {
        private List<VertexBufferElement> _elements = new List<VertexBufferElement>();
        private int _stride;

        public VertexBufferLayout()
        {
           
        }

        public void PushFloat(int count)
        {
            _elements.Add(new VertexBufferElement(VertexAttribPointerType.Float, count, false));
            _stride += count * VertexBufferElement.GetSizeOfType(VertexAttribPointerType.Float);
        }

        public void PushUInt(int count)
        {
            _elements.Add(new VertexBufferElement(VertexAttribPointerType.UnsignedInt, count, false));
            _stride += count * VertexBufferElement.GetSizeOfType(VertexAttribPointerType.UnsignedInt);
        }

        public void PushUChar(int count)
        {
            _elements.Add(new VertexBufferElement(VertexAttribPointerType.UnsignedByte, count, true));
            _stride += count * VertexBufferElement.GetSizeOfType(VertexAttribPointerType.UnsignedByte);
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
