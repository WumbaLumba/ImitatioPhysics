namespace ImitatioPhysics
{
    class Triangle : Shape
    {
        // Specific to the triangle class: 3 vertices
        protected float[] _positionsTriangle = new float[]
        {
               0.0f,   0.0f,  // 0 bottom-left
             100.0f,   0.0f,  // 1 bottom-right
             100.0f, 100.0f   // 2 top-middle
        };                   

        protected uint[] _indicesTriangle = new uint[]
        {
            0, 1, 2
        };

        public Triangle()
        {
            // Add the vertices and indices to the shape. 
            _positionsShape.AddRange(_positionsTriangle);
            _indicesShape.AddRange(_indicesTriangle);

            Create();
        }
    }
}
