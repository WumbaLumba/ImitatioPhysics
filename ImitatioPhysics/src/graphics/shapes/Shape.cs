namespace ImitatioPhysics
{
    class Shape
    {
        protected VertexBuffer _vertexBuffer;
        protected IndexBuffer _indexBuffer;
        protected VertexArray _vertexArray;
        protected VertexBufferLayout _layout;
        
        protected Shader _shader;
        protected Renderer _renderer = new Renderer();

        protected Shape()
        {

        }

    }
}
