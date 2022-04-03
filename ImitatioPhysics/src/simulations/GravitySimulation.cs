using ImGuiNET;
using System.Numerics;

namespace ImitatioPhysics
{
    class GravitySimulation : Simulation
    {
        public Vector4 SquareColor;
        public Vector3 Position;
        public Vector3 Velocity;

        public bool IsRunning = false;
        public bool Reset = false;
        public bool IsPaused = false;


        Quad _quad;

        //private ShapesToRender _shapes = new ShapesToRender();
        
        public GravitySimulation() : base()
        {
            //_shapes.AddShape("q1", _quad);
            _quad = new Quad();
            
        }

        public override void OnRender()
        {
            base.OnRender();

            _quad.Render();
        }

        public override void OnUpdate(float dt)
        {
            base.OnUpdate(dt);

            if (IsRunning)
            {
                _quad.Move(dt);
            }

            else
            {
                _quad.SetPosition(new OpenTK.Mathematics.Vector3(Position.X, Position.Y, Position.Z));
                _quad.SetPosition(new OpenTK.Mathematics.Vector3(Velocity.X / 0.0002645833f, Velocity.Y / 0.0002645833f, Velocity.Z));
               
            }
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
                ImGui.Text("Appearance");
                ImGui.ColorEdit4("Background Colour", ref _clearColor);
                ImGui.ColorEdit4("Square Colour", ref SquareColor);
                if (!IsRunning)
                {
                    ImGui.Text("\nInitial Position");
                    ImGui.SliderFloat("(P) X-axis", ref Position.X, 0.0f, 860.0f);
                    ImGui.SliderFloat("(P) Y-axis", ref Position.Y, 0.0f, 440.0f);

                    ImGui.Text("\nInitial Velocity");
                    ImGui.SliderFloat("(V) X-axis", ref Velocity.X, -5.0f, 5.0f);
                    ImGui.SliderFloat("(V) Y-axis", ref Velocity.Y, -5.0f, 5.0f);
                }
            }
            ImGui.End();

            ImGui.Begin("Instructions");
            {
                ImGui.Text(@"
Welcome to ImitatioPhysics!
Move the object to the top of the screen 
and run the simulation to let it drop.
                ");
            }
            ImGui.End();

            ImGui.Begin("Simulation Info");
            {
                ImGui.Text("(VELOCITY)\nX: " + _quad.GetVelocity().X * 0.0002645833f + "\nY: " + _quad.GetVelocity().Y * 0.0002645833f);
                ImGui.Text("Mass: " + _quad.GetMass() + " kg");
            }
            ImGui.End();

            base.OnImGuiRender();

        }

    }
}