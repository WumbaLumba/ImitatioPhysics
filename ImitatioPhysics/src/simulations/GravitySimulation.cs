using OpenTK.Graphics.OpenGL;
using ImitatioPhysics;
using ImGuiNET;
using System.Numerics;

namespace Simulations
{
    class GravitySimulation : Simulation
    {
        private Vector4 _clearColor;
        public Vector4 SquareColor;
        public Vector3 Position;

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

        private static physics.Particle _particle = new physics.Particle(new OpenTK.Mathematics.Vector3(0, 0, 0));

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

        public GravitySimulation() 
        {
            _clearColor = new Vector4(0.016f, 0.027f, 0.074f, 1.0f);
            SquareColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            Position = new Vector3(0.0f, 0.0f, 0.0f);

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
            GL.ClearColor(_clearColor.X, _clearColor.Y, _clearColor.Z, _clearColor.W);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            _shader.Bind();


            _model = OpenTK.Mathematics.Matrix4.CreateTranslation(_particle.Position);

            _mvp = _model * _view * _proj;

            _shader.SetUniformMat4("u_MVP", ref _mvp);
            _shader.SetUniform4("u_Color", SquareColor.X, SquareColor.Y, SquareColor.Z, SquareColor.W);

            _vertexArray.Bind();
            _indexBuffer.Bind();

            // Draw object.
            _renderer.Draw(ref _vertexArray, ref _indexBuffer, ref _shader);

            //SwapBuffers();
        }

        public override void OnUpdate(float dt)
        {
            base.OnUpdate(dt);

            if (IsRunning)
            {
                _particle.Update(dt);
                _model = OpenTK.Mathematics.Matrix4.CreateTranslation(_particle.Position);
            }

            else
            {
                _particle.Position = (Position.X, Position.Y, Position.Z);
                _particle.Velocity = (0.0f, 0.0f, 0.0f);
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
                ImGui.ColorEdit4("Background Colour", ref _clearColor);
                ImGui.ColorEdit4("Square Colour", ref SquareColor);
                if (!IsRunning)
                {
                    ImGui.SliderFloat("X-axis", ref Position.X, 0.0f, 860.0f);
                    ImGui.SliderFloat("Y-axis", ref Position.Y, 0.0f, 440.0f);
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
                ImGui.Text("(VELOCITY)\nX: " + _particle.Velocity.X + "\nY: " + _particle.Velocity.Y);
            }
            ImGui.End();

            base.OnImGuiRender();

        }

    }
}