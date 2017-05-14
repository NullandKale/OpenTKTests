﻿using System;
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
        public int width;
        public int height;
        TextureAtlas atlas;
        int texID;

        //Construct with single texture file
        public quad(string textureLocation)
        {
            pos = new transform();
            components = new List<iComponent>();
            tex = ContentPipe.LoadTexture(textureLocation, false);
            height = tex.height;
            width = tex.width;
            Game.window.UpdateFrame += update;
        }

        public quad(Texture2D texture)
        {
            pos = new transform();
            components = new List<iComponent>();
            tex = texture;
            height = tex.height;
            width = tex.width;
            Game.window.UpdateFrame += update;
        }

        //Construct with texture atlas and Texture ID
        public quad(TextureAtlas tAtlas, int id)
        {
            pos = new transform();
            components = new List<iComponent>();
            atlas = tAtlas;
            texID = id;
            tex = tAtlas.getTile(id);
            height = tAtlas.tilePixelHeight;
            width = tAtlas.tilePixelWidth;
            Game.window.UpdateFrame += update;
        }

        //Construct quad from texture
        public quad(text t)
        {
            pos = new transform();
            components = new List<iComponent>();
            tex = t.tex;
            width = t.tiles[0].tAtlas.tilePixelWidth * t.tiles.Length;
            height = t.tiles[0].tAtlas.tilePixelHeight;
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
            GL.Vertex2(width, height);

            GL.TexCoord2(tex.xStart, tex.yEnd);
            GL.Vertex2(0, height);

            GL.TexCoord2(tex.xStart, tex.yStart);
            GL.Vertex2(0, 0);

            GL.TexCoord2(tex.xEnd, tex.yStart);
            GL.Vertex2(width, 0);

            GL.TexCoord2(tex.xEnd, tex.yEnd);
            GL.Vertex2(width, height);

            GL.End();
        }
    }
}
