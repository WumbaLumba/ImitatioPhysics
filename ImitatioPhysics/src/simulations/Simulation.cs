using ImitatioPhysics;

namespace Simulations
{
    class Simulation
    {
        public Simulation()
        {

        }

        public virtual void OnUpdate(float dt)
        { }

        public virtual void OnRender()
        { }

        public virtual void OnImGuiRender()
        { }

        ~Simulation()
        {

        }
    }

}
