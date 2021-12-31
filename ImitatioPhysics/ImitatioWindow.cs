using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using ImGuiNET;

namespace ImitatioPhysics
{
    class ImitatioWindow : GameWindow
    {
        private VertexBuffer _vertexBuffer;
        private IndexBuffer _indexBuffer;
        private VertexArray _vertexArray;
        private VertexBufferLayout _layout;

        private Shader _shader;

        private Renderer _renderer = new Renderer();

        private float[] _positions = new float[]
        {
            0.5f,  0.5f, // 0
            0.5f, -0.5f, // 1
           -0.5f, -0.5f, // 2
           -0.5f,  0.5f, // 3
        };

        private uint[] _indices = new uint[]
        {
            0, 1, 2, 
            2, 3, 0
        };

        private int _location;
        private float r = 0.0f;
        private float _increment = 0.05f;

        public ImitatioWindow() : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            // Define the size of viewport.
            GL.Viewport(0, 0, e.Width, e.Height);
            base.OnResize(e);
        }

        protected override void OnLoad()
        {
            _vertexArray = new VertexArray();
            _vertexBuffer = new VertexBuffer(_positions, _positions.Length * sizeof(float));

            _layout = new VertexBufferLayout();
            _layout.PushFloat(2);
            _vertexArray.AddBuffer(_vertexBuffer, _layout);

            _indexBuffer = new IndexBuffer(_indices, _indices.Length);

            _shader = new Shader("shader.vert", "shader.frag");
            _shader.Bind();
            _shader.SetUniform4("u_Color", 0.2f, 0.3f, 0.8f, 1.0f);

            _vertexArray.Unbind();
            _shader.Unbind();
            _vertexArray.Unbind();
            _indexBuffer.UnBind();

            base.OnLoad();
        }


        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            // Clear screen using the colour set in OnLoad.
            _renderer.Clear();

            _shader.Bind();
            _shader.SetUniform4("u_Color", r, 0.3f, 0.8f, 1.0f);

            _vertexArray.Bind();
            _indexBuffer.Bind();

            _renderer.Draw(ref _vertexArray, ref _indexBuffer, ref _shader);

            if (r > 1.0f) _increment = -0.05f;
            else if (r < 0.0f) _increment = 0.05f;

            r += _increment;
            
            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            base.OnUnload();
        }
    }
}
  