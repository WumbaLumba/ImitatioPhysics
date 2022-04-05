using OpenTK.Mathematics;

namespace ImitatioPhysics
{
    class Square
    {
        public Vector4 Color;

        private float[] _vertices;
        private float _size;

        // Default translation/position for object to (0, 0).
        protected static Particle _particle = new Particle(new Vector3(0.0f, 0.0f, 0.0f));

        public Square(float x, float y)
        {
            // Default color to white.
            Color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            _particle.Position = new Vector3(x, y, 0.0f);
            // Default size to 100.
            _size = 100;

            UpdateVertices();
        }

        public void ChangeColor(Vector4 color)
        {
            Color = color;
        }

        public void UpdateVertices()
        {
            float x = _particle.Position.X;
            float y = _particle.Position.Y;

            _vertices = new float[24] {
                x,         y,         Color.X, Color.Y, Color.Z, Color.W,
                x + _size, y,         Color.X, Color.Y, Color.Z, Color.W,
                x + _size, y + _size, Color.X, Color.Y, Color.Z, Color.W,
                x,         y + _size, Color.X, Color.Y, Color.Z, Color.W
            };
        }
        public void Move(float dt)
        {
            _particle.Update(dt);
            UpdateVertices();
        }

        public ref Particle ReturnRef() => ref _particle;

        public float[] GetVertices() => _vertices;

        public Vector3 GetVelocity() => _particle.Velocity;

        public Vector3 GetPosition() => _particle.Position;

        public void SetVelocity(Vector3 vel) => _particle.Velocity = (vel.X, vel.Y, vel.Z);

        public void SetPosition(Vector3 pos) => _particle.Position = (pos.X, pos.Y, pos.Z);

        public float GetMass() => _particle.GetMass();
    }
}
