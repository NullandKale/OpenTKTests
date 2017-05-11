using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace OpenTKTest1
{
    public class quad : renderable
    {
        TextureAtlas atlas;
        int texID;

        //Construct with single texture file
        public quad(string textureLocation)
        {
            pos = new transform();
            components = new List<iComponent>();
            tex = ContentPipe.LoadTexture(textureLocation, false);
            Game.window.UpdateFrame += update;
        }

        //Construct with texture atlas
        public quad(TextureAtlas tAtlas, int id)
        {
            pos = new transform();
            components = new List<iComponent>();
            atlas = tAtlas;
            texID = id;
            tex = tAtlas.getTile(id);
            Game.window.UpdateFrame += update;
        }

        public override void update(object sender, FrameEventArgs e)
        {
            //Loop through all componants and run them.
            for(int i = 0; i < components.Count; i++)
            {
                components[i].Run(this);
            }

            //At end of update add renderer to render Queue.
            Game.renderQueue.Enqueue(render);
        }

        public override void render()
        {
            pos.updateMatrix();
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref pos.modelViewMatrix);

            //Replace GL command with cached texture set.
            //This function only sets the texture if it isnt already set.
            //GL.BindTexture(TextureTarget.Texture2D, tex.id);
            ContentPipe.GLSetTexture(tex.id);

            GL.Begin(PrimitiveType.Triangles);

            GL.TexCoord2(tex.xStart, tex.yStart);
            GL.Vertex2(0, 0);

            GL.TexCoord2(tex.xEnd, tex.yEnd);
            GL.Vertex2(64, 64);

            GL.TexCoord2(tex.xStart, tex.yEnd);
            GL.Vertex2(0, 64);

            GL.TexCoord2(tex.xStart, tex.yStart);
            GL.Vertex2(0, 0);

            GL.TexCoord2(tex.xEnd, tex.yStart);
            GL.Vertex2(64, 0);

            GL.TexCoord2(tex.xEnd, tex.yEnd);
            GL.Vertex2(64, 64);

            GL.End();
        }
    }
}
