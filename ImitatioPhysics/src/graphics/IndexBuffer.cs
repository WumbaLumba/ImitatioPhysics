using OpenTK.Graphics.OpenGL;

namespace ImitatioPhysics
{
    class IndexBuffer
    {
        // index buffer handle
        private int _rendererID;

        // no. elements in buffer
        private int _count;

        public IndexBuffer(uint[] data, int count)
        {
            _count = count;
            _rendererID = GL.GenBuffer();

            Bind();
            GL.BufferData(BufferTarget.ElementArrayBuffer, count * sizeof(uint), data, BufferUsageHint.StaticDraw);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _rendererID);
        }

        public void UnBind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public int GetCount()
        {
            return _count;
        }

        ~IndexBuffer()
        {
            GL.DeleteBuffer(_rendererID);
        }
    }
}
