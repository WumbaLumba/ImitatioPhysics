using OpenTK.Mathematics;

namespace ImitatioPhysics
{
    class Square
    {
        public System.Numerics.Vector4 Color = new System.Numerics.Vector4(1.0f, 1.0f, 1.0f, 1.0f);

        private float[] _vertices;
        private float _size;

        private float _massReciprocal, _damping = 1.0f;
        public Vector3 Position;
        public Vector3 Velocity;
        public Vector3 Acceleration;

        private Vector3 _resAcceleration;

        // Store value of all forces acting on the particle.
        private Vector3 _forceAcc;

        public Square(float x, float y)
        {
            Position = new Vector3(x, y, 0.0f);
            // Default color to white.
            Color = new System.Numerics.Vector4(1.0f, 1.0f, 1.0f, 1.0f);

            Position = new Vector3(0.0f, 0.0f, 0.0f);
            Velocity = (0.0f, 0.0f, 0.0f);

            // Default acceleration to gravitational acceleration constant.
            Acceleration = new Vector3(0.0f, -9.81f / 0.0002645833f, 0.0f);

            // Deafult resulting force to 0.
            _forceAcc = new Vector3(0.0f, 0.0f, 0.0f);

            // Default size to 100.
            _size = 100;

            _vertices = new float[8] {
                x,         y,
                x + _size, y,
                x + _size, y + _size,
                x,         y + _size
            };
        }

        public void ChangeColor(Vector4 color)
        {
            Color = new System.Numerics.Vector4(color.X, color.Y, color.Z, color.W);
        }

        public void SetMass(float mass)
        {
            if (mass > 0.0f)
                _massReciprocal = 1.0f / mass;
        }

        public float GetMass() => 1 / _massReciprocal;

        // Integrate initial velocity and acceleration to update current position and velocity.
        public void Update(float dt)
        {
            // Add acceleration due to forces other than gravity
            _resAcceleration = Acceleration;

            // This is in case the initial acceleration changes during update.
            _resAcceleration += _massReciprocal * _forceAcc;

            Position += (Velocity * _damping * dt) + (_resAcceleration * 0.5f * dt * dt);
            Velocity += _resAcceleration * dt;
        }

        public void SetDamping(float damping)
        {
            if (damping > 0.0f && damping < 1.0f)
                _damping = damping;
        }

        // Add force to the force accumulator.
        public void AddForce(Vector3 force) => _forceAcc += force;

        public float[] GetVertices() => _vertices;
    }
}
