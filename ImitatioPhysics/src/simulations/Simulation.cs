using ImitatioPhysics;
using ImGuiNET;
using physics;

namespace Simulations
{
    class Simulation
    {
        protected static float _width, _height;
        public Simulation()
        {

        }

        public virtual void OnResize(float width, float height)
        {
            _width = width;
            _height = height;
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
