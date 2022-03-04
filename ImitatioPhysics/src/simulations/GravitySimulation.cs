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

        private VertexBuffer _vertexBuffer;
        private IndexBuffer _indexBuffer;
        private VertexArray _vertexArray;
        private VertexBufferLayout _layout;

        private Shader _shader;
        private Renderer _renderer = new Renderer();
        //private Texture _texture;


        Quad quad = new Quad();

        private static Particle _particle = new Particle(new OpenTK.Mathematics.Vector3(0.0f, 0.0f, 0.0f));

        private float[] _positions = new float[]
        {
               0.0f,   0.0f,  // 0 bottom-left
             100.0f,   0.0f,  // 1 bottom-right
             100.0f, 100.0f,  // 2 top-right
               0.0f, 100.0f,  // 3 top-left
        };

        private uint[] _indices = new uint[]
        {
            0, 1, 2,
            2, 3, 0
        };

        private static OpenTK.Mathematics.Matrix4 _proj = 
            OpenTK.Mathematics.Matrix4.CreateOrthographicOffCenter(0.0f, 960.0f,    // left -> right
                                                                   0.0f, 540.0f,    // bottom -> top
                                                                  -1.0f, 1.0f);     // far -> near

        // Modify view to add camera here:
        private static OpenTK.Mathematics.Matrix4 _view = OpenTK.Mathematics.Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);
        private static OpenTK.Mathematics.Matrix4 _model = OpenTK.Mathematics.Matrix4.CreateTranslation(_particle.Position);
        private static OpenTK.Mathematics.Matrix4 _mvp = _model * _view * _proj;

        public GravitySimulation() : base()
        {
            SquareColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            Position = new Vector3(0.0f, 0.0f, 0.0f);
            Velocity = new Vector3(0.0f, 0.0f, 0.0f);

            _vertexArray = new VertexArray();
            _vertexBuffer = new VertexBuffer(_positions, _positions.Length * sizeof(float));

            _layout = new VertexBufferLayout();

            // Add 2D positions to layout.
            _layout.PushFloat(2);

            _vertexArray.AddBuffer(_vertexBuffer, _layout);

            _indexBuffer = new IndexBuffer(_indices, _indices.Length);

            _shader = new Shader("res/shaders/shader.vert", "res/shaders/shader.frag");
            _shader.Bind();
            _shader.SetUniformMat4("u_MVP", ref _mvp);

            _vertexArray.Unbind();
            _shader.Unbind();
            _vertexBuffer.UnBind();
            _indexBuffer.UnBind();

            
        }

        public override void OnRender()
        {
            base.OnRender();

            _shader.Bind();

            
            _model = OpenTK.Mathematics.Matrix4.CreateTranslation(_particle.Position);

            _mvp = _model * _view * _proj;

            _shader.SetUniformMat4("u_MVP", ref _mvp);
            _shader.SetUniform4("u_Color", SquareColor.X, SquareColor.Y, SquareColor.Z, SquareColor.W);

            _vertexArray.Bind();
            _indexBuffer.Bind();

            // Draw object.
            _renderer.Draw(ref _vertexArray, ref _indexBuffer, ref _shader);

            quad.Render();
            quad.Move(_particle.Position);

            //SwapBuffers();
        }

        public override void OnUpdate(float dt)
        {
            base.OnUpdate(dt);

            if (IsRunning)
            {
                _particle.Update(0.001f);
                _model = OpenTK.Mathematics.Matrix4.CreateTranslation(_particle.Position);
            }

            else
            {
                _particle.Position = (Position.X, Position.Y, Position.Z);
                _particle.Velocity = (Velocity.X / 0.0002645833f, Velocity.Y / 0.0002645833f, Velocity.Z);
                //_particle.Velocity = (0.0f, 0.0f, 0.0f);
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
                ImGui.Text("(VELOCITY)\nX: " + _particle.Velocity.X * 0.0002645833f + "\nY: " + _particle.Velocity.Y * 0.0002645833f);
            }
            ImGui.End();

            base.OnImGuiRender();

        }

    }
}