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
    public abstract class renderable
    {
        public transform pos;
        public List<triangle> verts;
        protected List<iComponent> components;
        public Texture2D tex;
        public Color col;
        public bool active = true;

        public abstract void update(object sender, FrameEventArgs e);
        public abstract void render();

        public void AddComponent(iComponent c)
        {
            //prevent adding multiple of the same component
            if(!components.Contains(c))
            {
                components.Add(c);
            }
        }
    }

    public class transform
    {
        public static int masterScale = 4;
        public float xPos = 0;
        public float yPos = 0;
        public float zPos = 0;

        public float rotZ = 0;

        public float xScale = 1;
        public float yScale = 1;

        public Matrix4 modelViewMatrix;

        public void updateMatrix()
        {
              modelViewMatrix = Matrix4.CreateScale(xScale * masterScale, yScale * masterScale, 1f) * 
                Matrix4.CreateRotationZ(rotZ) * 
                Matrix4.CreateTranslation(xPos, yPos, zPos);
        }
    }

    public struct triangle
    {
        public Vector2 a;
        public Vector2 b;
        public Vector2 c;
    }
}
