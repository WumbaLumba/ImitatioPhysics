using OpenTK.Graphics.OpenGL;

namespace ImitatioPhysics
{
    class Renderer
    {
        public void Draw(ref VertexArray vertexArray, ref IndexBuffer indexBuffer, ref Shader shader)
        {
            shader.Bind();
            vertexArray.Bind();
            indexBuffer.Bind();

            GL.DrawElements(PrimitiveType.Triangles, indexBuffer.GetCount(), DrawElementsType.UnsignedInt, 0);
        }

        public void Clear() 
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }
    }
}
