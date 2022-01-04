﻿using OpenTK.Windowing.Desktop;
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

        private Texture _texture;

        private float[] _positions = new float[]
        {
             100.0f, 100.0f, 0.0f, 0.0f, // 0 bottom-left
             200.5f, 100.0f, 1.0f, 0.0f, // 1 bottom-right
             200.0f, 200.0f, 1.0f, 1.0f, // 2 top-right
             100.0f, 200.0f, 0.0f, 1.0f  // 3 top-left
        };

        private uint[] _indices = new uint[]
        {
            0, 1, 2, 
            2, 3, 0
        };

        private static Matrix4 _proj = Matrix4.CreateOrthographicOffCenter(0.0f, 960.0f,    // left -> right
                                                                           0.0f, 540.0f,    // bottom -> top
                                                                          -1.0f,   1.0f);   // far -> near

        private static Matrix4 _view = Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);

        private static Matrix4 _model = Matrix4.CreateTranslation(200, 200, 0);
        //private static Matrix4 matRot = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(-90.0f));

        private static Matrix4 _mvp = _model * _view * _proj; 

        public ImitatioWindow(int width, int height, string title) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.Title = title;
            this.Size = new Vector2i(width, height);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            // Define the size of viewport.
            GL.Viewport(0, 0, e.Width, e.Height);
            base.OnResize(e);
        }

        protected override void OnLoad()
        {
            // For transparent objects.
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            
            _vertexArray = new VertexArray();
            _vertexBuffer = new VertexBuffer(_positions, _positions.Length * sizeof(float));

            _layout = new VertexBufferLayout();
            // Add positions.
            _layout.PushFloat(2);
            // Add texture coordinates.
            _layout.PushFloat(2);
            _vertexArray.AddBuffer(_vertexBuffer, _layout);

            _indexBuffer = new IndexBuffer(_indices, _indices.Length);

            _shader = new Shader("res/shaders/shader.vert", "res/shaders/shader.frag");
            _shader.Bind();
            //_shader.SetUniform4("u_Color", 0.2f, 0.3f, 0.8f, 1.0f);
            _shader.SetUniformMat4("u_MVP", ref _mvp);

            _texture = new Texture("res/textures/eu.png");
            _texture.Bind(0);
            _shader.SetUniform1("u_Texture", 0);

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

            _vertexArray.Bind();
            _indexBuffer.Bind();

            _renderer.Draw(ref _vertexArray, ref _indexBuffer, ref _shader);
            
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
  