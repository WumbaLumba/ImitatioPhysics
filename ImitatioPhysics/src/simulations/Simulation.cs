using ImGuiNET;
using OpenTK.Graphics.OpenGL;
using System.Numerics;

namespace ImitatioPhysics
{
    class Simulation
    {
        protected static float _width, _height;
        protected Vector4 _clearColor;
        public Simulation()
        {
            _clearColor = new Vector4(0.016f, 0.027f, 0.074f, 1.0f);
        }

        public virtual void OnResize(float width, float height)
        {
            _width = width;
            _height = height;
        }

        public virtual void OnLoad()
        { }

        public virtual void OnUpdate(float dt)
        { }

        public virtual void OnRender()
        {
            GL.ClearColor(_clearColor.X, _clearColor.Y, _clearColor.Z, _clearColor.W);
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        public virtual void OnImGuiRender()
        {
            ImGui.Begin("Simulation Info");
            {
                ImGui.Text("Framerate: " + ImGui.GetIO().Framerate);
            }
            ImGui.End();
        }

        ~Simulation()
        {

        }
    }

}
