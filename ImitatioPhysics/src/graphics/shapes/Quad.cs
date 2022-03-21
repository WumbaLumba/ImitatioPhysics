namespace ImitatioPhysics
{
    class Quad : Shape
    {
        // Specific to the quad class -> 4 vertices
        protected float[] _positionsQuad = new float[]
        {
               0.0f,   0.0f,  // 0 bottom-left
             100.0f,   0.0f,  // 1 bottom-right
             100.0f, 100.0f,  // 2 top-right
               0.0f, 100.0f   // 3 top-left
        };

        protected uint[] _indicesQuad = new uint[]
        {
            0, 1, 2,
            2, 3, 0
        };

        public Quad()
        {
            _positionsShape.AddRange(_positionsQuad);
            _indicesShape.AddRange(_indicesQuad);
        }
    }
}
