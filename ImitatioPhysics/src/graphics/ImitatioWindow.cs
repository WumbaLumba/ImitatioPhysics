using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Simulations;
using ImGuiSample;

namespace ImitatioPhysics
{
    class ImitatioWindow : GameWindow
    {
        private ImGuiController _controller;
        private static GravitySimulation _sim = new GravitySimulation();

        
        public ImitatioWindow() : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = new Vector2i(960, 540) })
        {
            Title = "Imitatio Physics";
        }

        // Runs only once.
        protected override void OnLoad()
        {
            base.OnLoad();

            _sim.OnResize(ClientSize.X, ClientSize.Y);
            _controller = new ImGuiController(ClientSize.X, ClientSize.Y);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            _controller.Update(this, (float)e.Time);

            _sim.OnUpdate((float)e.Time);
            _sim.OnRender();
            _sim.OnImGuiRender();

            _controller.Render();

            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            // Set viewport as size of the window.
            _sim.OnResize(ClientSize.X, ClientSize.Y);
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