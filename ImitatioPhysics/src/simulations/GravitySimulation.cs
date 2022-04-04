using ImGuiNET;
using System.Numerics;

namespace ImitatioPhysics
{
    class GravitySimulation : Simulation
    {
        // Default to white
        public Vector4 SquareColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        public Vector3 Position;
        public Vector3 Velocity;

        public bool IsRunning = false;
        public bool Reset = false;
        public bool IsPaused = false;


        //Quad _quad;
        //Triangle _triangle;

        private Square _square;
        //private ShapesToRender _shapes = new ShapesToRender();
        
        public GravitySimulation() : base()
        {
            //_shapes.AddShape("q1", _quad);
            //_quad = new Quad();

            //_triangle = new Triangle();

            _square = new Square();

            Position = new Vector3(0.0f, 0.0f, 0.0f);
            Velocity = new Vector3(0.0f, 0.0f, 0.0f);;
        }

        public override void OnRender()
        {
            base.OnRender();

            _square.ChangeColor((SquareColor.X, SquareColor.Y, SquareColor.Z, SquareColor.W));

            _square.Render();

            
            //_triangle.Render();
        }

        public override void OnUpdate(float dt)
        {
            base.OnUpdate(dt);

            if (IsRunning)
            {
                _square.Move(dt);
            }

            else
            {
                _square.SetPosition(new OpenTK.Mathematics.Vector3(Position.X, Position.Y, Position.Z));
                _square.SetVelocity(new OpenTK.Mathematics.Vector3(Velocity.X / 0.0002645833f, Velocity.Y / 0.0002645833f, Velocity.Z));
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
                ImGui.Text("(VELOCITY1)\nX: " + _square.GetVelocity().X * 0.0002645833f + "\nY: " + _square.GetVelocity().Y * 0.0002645833f);
            }
            ImGui.End();

            base.OnImGuiRender();

        }

    }
}