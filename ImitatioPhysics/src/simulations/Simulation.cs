using ImitatioPhysics;
using ImGuiNET;
using physics;

namespace Simulations
{
    class Simulation
    {
        public Simulation()
        {

        }


        public virtual void OnLoad()
        { }

        public virtual void OnUpdate(float dt)
        { }

        public virtual void OnRender()
        { }

        public virtual void OnImGuiRender()
        {
            ImGui.Begin("Simulation Info");
            {
                ImGui.Text("Framerate: " + ImGui.GetIO().Framerate);
            }
            ImGui.End();
        }

        ~Simulation()
        {

        }
    }

}
