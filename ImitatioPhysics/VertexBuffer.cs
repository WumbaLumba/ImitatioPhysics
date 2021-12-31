﻿using OpenTK.Graphics.OpenGL;

namespace ImitatioPhysics
{
    class VertexBuffer
    {
        private int _rendererID;
        
        public VertexBuffer(float[] data, int size)
        {
            _rendererID = GL.GenBuffer();

            Bind();
            GL.BufferData(BufferTarget.ArrayBuffer, size, data, BufferUsageHint.StaticDraw);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, _rendererID);
        }

        public void UnBind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        ~VertexBuffer()
        {
            GL.DeleteBuffer(_rendererID);
        }
    }
}
