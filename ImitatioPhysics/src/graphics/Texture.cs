using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ImitatioPhysics
{
    class Texture
    {
        private int _rendererID;
        private string _filePath; // for debugging

        private Image<Rgba32> _textureImage;
        private List<byte> _texturePixels; //= new List<byte> ();

        public Texture(string filePath) 
        {   
            _rendererID = 0;
            _filePath = filePath;

            // Load image.
            _textureImage = Image.Load<Rgba32>(filePath);

            // OpenGL loads from the bottom and flips the image.
            // This flips it so that it is displayed properly.
            _textureImage.Mutate(x => x.Flip(FlipMode.Vertical));

            // 4 channels for rgba
            _texturePixels = new List<byte>(4 * _textureImage.Width * _textureImage.Height);

            // Convert image into a byte array so it can be used by OpenGL.
            for(int y = 0; y < _textureImage.Height; y++)
            {
                Span<Rgba32> pixelRowSpan = _textureImage.GetPixelRowSpan(y); 
                for(int x = 0; x < _textureImage.Width; x++)
                {
                    _texturePixels.Add(pixelRowSpan[x].R);
                    _texturePixels.Add(pixelRowSpan[x].G);
                    _texturePixels.Add(pixelRowSpan[x].B);
                    _texturePixels.Add(pixelRowSpan[x].A);
                }
            }

            // Generate texture and set up buffer.
            _rendererID = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, _rendererID);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

            // Add data to buffer.
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, _textureImage.Width, _textureImage.Height, 
                0, PixelFormat.Rgba, PixelType.UnsignedByte, _texturePixels.ToArray());
            
            UnBind();
        }

        public void Bind(int slot)
        {
            // Specifies which texture unit to make active.
            GL.ActiveTexture(TextureUnit.Texture0 + slot);
            GL.BindTexture(TextureTarget.Texture2D, _rendererID);
        }

        private void UnBind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        ~Texture()
        {
            GL.DeleteTexture(_rendererID);
        }
    }
}
