using OpenTK.Graphics.OpenGL;

namespace ImitatioPhysics
{
    class VertexArray
    {
        private int _rendererID;
        public VertexArray()
        {
            _rendererID = GL.GenVertexArray();
        }

        public void AddBuffer(VertexBuffer vertexBuffer, VertexBufferLayout layout)
        {
            Bind();
            vertexBuffer.Bind();
            List<VertexBufferElement> elements = layout.GetElements();
            
            int offset = 0;

            for (int i = 0; i < elements.Count; i++)
            {
                VertexBufferElement element = elements[i];
                GL.EnableVertexAttribArray(i);
                GL.VertexAttribPointer(i, element.Count, element.Type, element.Normalized, layout.GetStride(), offset);
                offset += element.Count * VertexBufferElement.GetSizeOfType(element.Type);
            }
        }

        public void Bind()
        {
            GL.BindVertexArray(_rendererID);
        }

        public void Unbind()
        {
            GL.BindVertexArray(0);
        }
        ~VertexArray()
        {
            GL.DeleteVertexArray(_rendererID);
        }
    }
}
