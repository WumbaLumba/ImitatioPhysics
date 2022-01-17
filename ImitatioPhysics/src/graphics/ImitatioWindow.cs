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
            _indexBuffer.UnBind();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            // Clear background buffer.
            _renderer.Clear();

            _controller.Update(this, (float)e.Time);

            /*
             * Code for ImGui OnRender and drawing goes here
             */



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
  
