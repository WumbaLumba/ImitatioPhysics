using OpenTK.Graphics.OpenGL;
using ImitatioPhysics;
using ImGuiNET;
using System.Numerics;
using System.Collections.Generic;

namespace Simulations
{
    class EmptySimulation : Simulation
    {
        private List<Shape> _shapes = new List<Shape>();
        private Vector4 _clearColor;
        public Vector4 SquareColor;

        public float PosX;
        public float PosY;

        public EmptySimulation()
        {
            _clearColor = new Vector4(0.016f, 0.027f, 0.074f, 1.0f);
            SquareColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        }

        public void AddShape(Shape shape)
        {
            _shapes.Add(shape);
        }


        public override void OnUpdate(float dt)
        {

        }

        public override void OnRender()
        {
            GL.ClearColor(_clearColor.X, _clearColor.Y, _clearColor.Z, _clearColor.W);
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        public override void OnImGuiRender()
        {
            ImGui.ColorEdit4("Background Colour", ref _clearColor);
            ImGui.ColorEdit4("Square Colour", ref SquareColor);
            ImGui.SliderFloat("X: ", ref PosX, 0.0f, 960.0f);
            ImGui.SliderFloat("Y: ", ref PosY, 0.0f, 540.0f);

        }

    }
}