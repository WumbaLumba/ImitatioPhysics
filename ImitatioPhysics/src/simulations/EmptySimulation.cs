using OpenTK.Graphics.OpenGL;
using ImGuiNET;
using System.Numerics;

namespace ImitatioPhysics
{
    class EmptySimulation : Simulation
    {
        public EmptySimulation()
        {
        }

        public override void OnUpdate(float dt)
        {

        }

        public override void OnRender()
        {
        }

        public override void OnImGuiRender()
        {
            base.OnImGuiRender();

        }

    }
}