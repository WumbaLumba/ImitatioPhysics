using ImGuiNET;
using System.Numerics;

namespace ImitatioPhysics
{
    class GravitySimulation : Simulation
    {

        public bool IsRunning = false;
        public bool Reset = false;
        public bool IsPaused = false;
        private ShapesToRender _shapes;
        
        public GravitySimulation() : base()
        {
            _shapes = new ShapesToRender();
            Square s1 = new Square(0.0f, 0.0f);
            Square s2 = new Square(200.0f, 0.0f);
            Square s3 = new Square(400.0f, 0.0f);
            _shapes.AddShape(s1);
            _shapes.AddShape(s2);
            _shapes.AddShape(s3);
        }

        public override void OnRender()
        {
            base.OnRender();
            _shapes.Render();

            //_square.ChangeColor((SquareColor.X, SquareColor.Y, SquareColor.Z, SquareColor.W));

            //_square.Render();

            
            //_triangle.Render();
        }

        public override void OnUpdate(float dt)
        {
            base.OnUpdate(dt);

            if (IsRunning)
            {
                //_square.Move(dt);
                for(int i = 0; i < _shapes.GetListSquares().Count; i++)
                {
                    _shapes.GetListSquares()[i].Move(dt);
                }
            }

            /*
            else
            {
                for (int i = 0; i < _shapes.GetListSquares().Count; i++)
                {
                    _shapes.GetListSquares()[i].SetPosition(new OpenTK.Mathematics.Vector3(Position.X, Position.Y, Position.Z));
                    _shapes.GetListSquares()[i].SetVelocity(new OpenTK.Mathematics.Vector3(Velocity.X / 0.0002645833f, Velocity.Y / 0.0002645833f, Velocity.Z));
                }
                //_square.SetPosition(new OpenTK.Mathematics.Vector3(Position.X, Position.Y, Position.Z));
                //_square.SetVelocity(new OpenTK.Mathematics.Vector3(Velocity.X / 0.0002645833f, Velocity.Y / 0.0002645833f, Velocity.Z));
            }
            */
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

                if (!IsRunning)
                {
                    Vector3 position, velocity;
                    Vector4 color;

                    for(int i = 0; i < _shapes.GetListSquares().Count; i++)
                    {
                        Square square = _shapes.GetListSquares()[i];

                        position = new Vector3(square.GetPosition().X, square.GetPosition().Y, square.GetPosition().Z);
                        velocity = new Vector3(square.GetVelocity().X, square.GetVelocity().Y, square.GetVelocity().Z);
                        color = new Vector4(square.Color.X, square.Color.Y, square.Color.Z, square.Color.W);
                        
                        ImGui.ColorEdit4("Square Colour", ref color);

                        ImGui.Text($"\nInitial Position {i}");
                        ImGui.SliderFloat($"(P{i}) X-axis", ref position.X, 0.0f, 860.0f);
                        ImGui.SliderFloat($"(P{i}) Y-axis", ref position.Y, 0.0f, 440.0f);

                        ImGui.Text($"\nInitial Velocity {i}");
                        ImGui.SliderFloat($"(V{i}) X-axis", ref velocity.X, -5.0f, 5.0f);
                        ImGui.SliderFloat($"(V{i}) Y-axis", ref velocity.Y, -5.0f, 5.0f);

                        _shapes.GetListSquares()[i].ChangeColor((color.X, color.Y, color.Z, color.W));
                        _shapes.GetListSquares()[i].SetPosition((position.X, position.Y, position.Z));
                        _shapes.GetListSquares()[i].SetVelocity((velocity.X / 0.0002645833f, velocity.Y / 0.0002645833f, velocity.Z));
                    }
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
                //ImGui.Text("(VELOCITY1)\nX: " + _square.GetVelocity().X * 0.0002645833f + "\nY: " + _square.GetVelocity().Y * 0.0002645833f);
            }
            ImGui.End();

            base.OnImGuiRender();

        }

    }
}