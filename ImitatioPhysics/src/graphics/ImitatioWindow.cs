using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using ImGuiNET;
using Simulations;
using ImGuiSample;

namespace ImitatioPhysics
{
    class ImitatioWindow : GameWindow
    {
        private ImGuiController _controller;
        private EmptySimulation _sim;
        
        private VertexBuffer _vertexBuffer;
        private IndexBuffer _indexBuffer;
        private VertexArray _vertexArray;
        private VertexBufferLayout _layout;

        private Shader _shader;
        private Renderer _renderer = new Renderer();
        //private Texture _texture;

        private static physics.Particle _particle = new physics.Particle(new Vector3(100, 100, 0));

        private float[] _positions = new float[]
        {
             100.0f, 100.0f,  // 0 bottom-left
             200.0f, 100.0f,  // 1 bottom-right
             200.0f, 200.0f,  // 2 top-right
             100.0f, 200.0f,  // 3 top-left
        };

        private uint[] _indices = new uint[]
        {
            0, 1, 2,
            2, 3, 0
        };

        private static Matrix4 _proj = Matrix4.CreateOrthographicOffCenter(0.0f, 960.0f,    // left -> right
                                                                           0.0f, 540.0f,    // bottom -> top
                                                                          -1.0f,   1.0f);   // far -> near

        // Modify view and translation here:
        private static Matrix4 _view = Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);
        private static Vector3 _translation = new Vector3(0.0f, 0.0f, 0.0f);
        private static Matrix4 _model = Matrix4.CreateTranslation(_particle.Positon);

        private static Matrix4 _mvp = _model * _view * _proj;

        public ImitatioWindow() : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = new Vector2i(960, 540) })
        {
            Title = "Imitatio Physics";
        }

        // Runs only once.
        protected override void OnLoad()
        {
            base.OnLoad();

            _sim = new EmptySimulation();
            _controller = new ImGuiController(ClientSize.X, ClientSize.Y);

            // Enable blending for transparent objects.
            // Used previously to load pngs properly.
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

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
            _vertexArray.Unbind();
            _indexBuffer.UnBind();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            // Clear background buffer.
            _renderer.Clear();

            _controller.Update(this, (float)e.Time);

            _sim.OnUpdate(0.0f);
            _sim.OnRender();
            _sim.OnImGuiRender();

            // Set new colour using slider.
            // TODO: Move object on screen in real time.
            _particle.Update((float)e.Time);
            _shader.Bind();
            
            // Solved? Nope.
            // Doesn't update model matrix when the position of the particle is updated.
            //_model = Matrix4.CreateTranslation(_particle.Positon);
            _mvp = _model * _view * _proj;
            _shader.SetUniformMat4("u_MVP", ref _mvp);
            _shader.SetUniform4("u_Color", _sim.SquareColor.X, _sim.SquareColor.Y, _sim.SquareColor.Z, _sim.SquareColor.W);

            _vertexArray.Bind();
            _indexBuffer.Bind();

            // Draw object.
            _renderer.Draw(ref _vertexArray, ref _indexBuffer, ref _shader);

            _controller.Render();

            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            // Set viewport as size of the window.
            GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
            _controller.WindowResized(ClientSize.X, ClientSize.Y);
        }

        // From ImGui sample
        protected override void OnTextInput(TextInputEventArgs e)
        {
            base.OnTextInput(e);


            _controller.PressChar((char)e.Unicode);
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            base.OnUnload();
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            _controller.MouseScroll(e.Offset);
        }
    }
}
  
