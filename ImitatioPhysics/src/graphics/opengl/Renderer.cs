using OpenTK.Graphics.OpenGL;

namespace ImitatioPhysics
{
    class Renderer
    {
        // Draw vertices specified by the index buffer using the data and layout from vertex array.
        public void Draw(ref VertexArray vertexArray, ref IndexBuffer indexBuffer, ref Shader shader)
        {
            shader.Bind();
            vertexArray.Bind();
            indexBuffer.Bind();

            GL.DrawElements(PrimitiveType.Triangles, indexBuffer.GetCount(), DrawElementsType.UnsignedInt, 0);
        }

        // Clear background buffer.
        public void Clear() 
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }
    }
}



