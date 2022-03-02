using OpenTK.Mathematics;

namespace physics
{
    class Particle
    {
        protected float _massReciprocal, _damping  = 1.0f;
        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }
        public Vector3 Acceleration { get; set; }

        private Vector3 _gravAcc = new Vector3(0.0f, -9.81f / 0.0002645833f, 0.0f);

        public Particle(Vector3 position)
        {
            Position = position;
            Velocity = (0.0f, 0.0f, 0.0f);
            Acceleration = _gravAcc;
        }

        // Set mass using the inverse.
        public void SetMass(float mass)
        {
            if (mass > 0.0f)
                _massReciprocal = 1.0f / mass;
        }

        // Set damping.
        public void SetDamping(float damping)
        {
            if (damping > 0.0f && damping < 1.0f)
                _damping = damping;
        }

        // Integrate initial velocity and acceleration to update current position and velocity.
        public void Update(float dt)
        {
            Position += (Velocity * _damping  * dt) + (Acceleration * 0.5f * dt * dt);
            Velocity += Acceleration * dt;
        }
    }
}




