﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenTKTest1
{
    public class text : renderable
    {
        public Tile[] tiles;

        public text(letter[] letters)
        {
            tiles = new Tile[letters.Length];
            pos = new transform();
            components = new List<iComponent>();

            for(int i = 0; i < letters.Length; i++)
            {
                tiles[i] = new Tile();
                tiles[i].TexID = (int)letters[i];
                tiles[i].tAtlas = Game.font;
            }

            tex = TextureManager.TextureFrom1DTileMap(tiles);
            Game.window.UpdateFrame += update;
        }

        public text(string s)
        {

            letter[] letters = stringToLetter(s);
            tiles = new Tile[letters.Length];
            pos = new transform();
            components = new List<iComponent>();

            for (int i = 0; i < letters.Length; i++)
            {
                tiles[i] = new Tile();
                tiles[i].TexID = (int)letters[i];
                tiles[i].tAtlas = Game.font;
            }

            tex = TextureManager.TextureFrom1DTileMap(tiles);
            Game.window.UpdateFrame += update;
        }

        public override void update(object sender, FrameEventArgs e)
        {
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
            TextureManager.GLSetTexture(tex.id);

            GL.Begin(PrimitiveType.Triangles);

            GL.TexCoord2(tex.xStart, tex.yStart);
            GL.Vertex2(0, 0);

            GL.TexCoord2(tex.xEnd, tex.yEnd);
            GL.Vertex2(tex.width, tex.height);

            GL.TexCoord2(tex.xStart, tex.yEnd);
            GL.Vertex2(0, tex.height);

            GL.TexCoord2(tex.xStart, tex.yStart);
            GL.Vertex2(0, 0);

            GL.TexCoord2(tex.xEnd, tex.yStart);
            GL.Vertex2(tex.width, 0);

            GL.TexCoord2(tex.xEnd, tex.yEnd);
            GL.Vertex2(tex.width, tex.height);

            GL.End();
        }

        public static letter[] stringToLetter(string s)
        {
            char[] c = s.ToCharArray();
            letter[] l = new letter[c.Length];

            for(int i = 0; i < c.Length; i++)
            {
                l[i] = charToLetter(c[i]);
            }

            return l;
        }

        public static letter charToLetter(char c)
        {
            return (letter)((int)c - (int)' ');
        }
    }
}
