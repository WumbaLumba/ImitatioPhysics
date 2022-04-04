using OpenTK.Graphics.OpenGL;

namespace ImitatioPhysics
{
    class VertexBuffer
    {
        // buffer handle
        private int _rendererID;
        
        public VertexBuffer(int size)
        {
            _rendererID = GL.GenBuffer();
            Bind();

            // Don't supply buffer with any data.
            GL.BufferData(BufferTarget.ArrayBuffer, size, IntPtr.Zero, BufferUsageHint.DynamicDraw);
        }

        public void UpdateData(float[] data)
        {
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, data.Length * sizeof(float), data);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, _rendererID);
        }

        public void UnBind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        ~VertexBuffer()
        {
            GL.DeleteBuffer(_rendererID);
        }
    }
}