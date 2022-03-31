using OpenTK.Mathematics;

namespace ImitatioPhysics
{
    class Particle
    {
        protected float _massReciprocal, _damping  = 1.0f;
        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }
        public Vector3 Acceleration { get; set; }
        private Vector3 _resAcceleration;

        // Store value of all forces acting on the particle.
        private Vector3 _forceAcc { get; set; }

        public Particle(Vector3 position)
        {
            Position = position;
            Velocity = (0.0f, 0.0f, 0.0f);

            // Default acceleration to gravitational acceleration constant.
            Acceleration = new Vector3(0.0f, -9.81f / 0.0002645833f, 0.0f);

            // Deafult resulting force to 0.
            _forceAcc = new Vector3(0.0f, 0.0f, 0.0f);
        }

        // Set mass using the inverse
        public void SetMass(float mass)
        {
            if (mass > 0.0f)
                _massReciprocal = 1.0f / mass;
        }

        public float GetMass() => 1 / _massReciprocal;

        // Set damping.
        public void SetDamping(float damping)
        {
            if (damping > 0.0f && damping < 1.0f)
                _damping = damping;
        }

        // Integrate initial velocity and acceleration to update current position and velocity.
        public void Update(float dt)
        {
            // Add acceleration due to forces other than gravity
            _resAcceleration = Acceleration;

            // This is in case the initial acceleration changes during update.
            _resAcceleration += _massReciprocal * _forceAcc;

            Position += (Velocity * _damping  * dt) + (_resAcceleration * 0.5f * dt * dt);
            Velocity += _resAcceleration * dt;
        }

        // Add force to the force accumulator.
        public void AddForce(Vector3 force) => _forceAcc += force;
    }
}




