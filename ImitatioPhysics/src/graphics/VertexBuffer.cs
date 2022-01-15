using OpenTK.Graphics.OpenGL;

namespace ImitatioPhysics
{
    class VertexBuffer
    {
        // buffer handle
        private int _rendererID;
        
        public VertexBuffer(float[] data, int size)
        {
            _rendererID = GL.GenBuffer();
            Bind();

            GL.BufferData(BufferTarget.ArrayBuffer, size, data, BufferUsageHint.DynamicDraw);
        }

        public void UpdateData()
        {

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
