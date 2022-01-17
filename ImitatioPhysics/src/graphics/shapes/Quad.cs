using OpenTK.Mathematics;

namespace ImitatioPhysics
{
    class Quad : Shape
    {
        private static float _width;
        private static float _height;
        private static Vector3 _centerCoord;

        private float[] _positions = new float[]
        {
            _centerCoord.X - (_width/2), _centerCoord.Y - (_height/2),  // 0 bottom-left
            _centerCoord.X + (_width/2), _centerCoord.Y - (_height/2),  // 1 bottom-right
            _centerCoord.X + (_width/2), _centerCoord.Y + (_height/2),  // 2 top-right
            _centerCoord.X - (_width/2), _centerCoord.Y + (_height/2)   // 3 top-left
        };

        private uint[] _indices = new uint[]
        {
            0, 1, 2,
            2, 3, 0
        };

        public Quad(float width, float height)
        {
            _width = width;
            _height = height;
        }

        public void SetCenterCoord(Vector3 centerCoord) => _centerCoord = centerCoord;

        public float[] GetPositions() => _positions;

        public uint[] GetIndices() => _indices;

        public void SetSize(float width, float height)
        {
            _width = width;
            _height = height;
        }
    }
}
