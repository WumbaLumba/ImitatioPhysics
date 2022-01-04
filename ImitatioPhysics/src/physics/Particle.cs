using System;
using OpenTK.Mathematics;


namespace physics
{
    class Particle
    {
        protected float _massReciprocal, _damping  = 1.0f;
        public Vector3 Positon { get; set; }
        public Vector3 Velocity { get; set; }
        public Vector3 Acceleration { get; set; }

        public Particle(Vector3 position)
        {
            Positon = position;
            Velocity = (10.0f, 0.0f, 0.0f);
            Acceleration = (0.0f, 0.0f, 0.0f);
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


        // TODO: link duration to time between frame.
        // Integrate initial velocity and acceleration to update current position and velocity.
        public void Update(float dt)
        {
            Positon += (Velocity * _damping  * dt) + (Acceleration * 0.5f * dt * dt);
            Velocity += Acceleration * dt;
        }
    }
}




