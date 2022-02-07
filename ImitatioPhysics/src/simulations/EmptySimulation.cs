using OpenTK.Graphics.OpenGL;
using ImitatioPhysics;
using ImGuiNET;
using System.Numerics;

namespace Simulations
{
    class EmptySimulation : Simulation
    {
        private Vector4 _clearColor;
        public Vector4 SquareColor;
        public Vector3 Position;

        public bool IsRunning = false;
        public bool Reset = false;

        public EmptySimulation()
        {
            _clearColor = new Vector4(0.016f, 0.027f, 0.074f, 1.0f);
            SquareColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            Position = new Vector3(0.0f, 0.0f, 0.0f);
        }

        public override void OnUpdate(float dt)
        {

        }

        public override void OnRender()
        {
            GL.ClearColor(_clearColor.X, _clearColor.Y, _clearColor.Z, _clearColor.W);
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        public override void OnImGuiRender()
        {
            ImGui.Begin("Simulation Control");
            {
                if (!IsRunning)
                    IsRunning = ImGui.Button("Run");
                else
                    IsRunning = !ImGui.Button("Restart");
            }
            ImGui.End();

            ImGui.Begin("Properties");
            {
                ImGui.ColorEdit4("Background Colour", ref _clearColor);
                ImGui.ColorEdit4("Square Colour", ref SquareColor);
                if (!IsRunning || Reset)
                {
                    ImGui.SliderFloat("X-axis", ref Position.X, 0.0f, 860.0f);
                    ImGui.SliderFloat("Y-axis", ref Position.Y, 0.0f, 440.0f);
                }
            }
            ImGui.End();

            ImGui.Begin("Info");
            {
                ImGui.Text(@"
Welcome to ImitatioPhysics!
Move the object to the top of the screen 
and run the simulation to let it drop.
                ");
            }
            ImGui.End();
            base.OnImGuiRender();

        }

    }
}