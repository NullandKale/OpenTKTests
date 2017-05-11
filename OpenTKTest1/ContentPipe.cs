using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;

namespace OpenTKTest1
{
    class ContentPipe
    {
        static int currentTexture = -1;

        public static Texture2D LoadTexture(string filePath, bool LinearFiltering)
        {
            Bitmap bitmap = new Bitmap(filePath);
            int id = GL.GenTexture();

            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.BindTexture(TextureTarget.Texture2D, id);

            GL.TexImage2D(TextureTarget.Texture2D, 0,
                PixelInternalFormat.Rgba, bitmap.Width, bitmap.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                PixelType.UnsignedByte, bmpData.Scan0);

            bitmap.UnlockBits(bmpData);

            if(LinearFiltering)
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                    (int)TextureMinFilter.Linear);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                    (int)TextureMagFilter.Linear);
            }
            else
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                    (int)TextureMinFilter.Nearest);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                    (int)TextureMinFilter.Nearest);
            }

            return new Texture2D(id, bitmap.Width, bitmap.Height);
        }

        public static void GLSetTexture(int id)
        {
            if(currentTexture == -1 || currentTexture != id)
            {
                GL.BindTexture(TextureTarget.Texture2D, id);
                currentTexture = id;
            }
        }
    }
}
