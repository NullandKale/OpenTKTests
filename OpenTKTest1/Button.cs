﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Input;
using System.Drawing;

namespace OpenTKTest1
{
    class Button
    {
        public text t;
        public quad background;
        public MouseButton button;
        public Action onClick;

        private String echo;

        public Button(string text, Texture2D background, Action onClick, MouseButton buttonToCheck)
        {
            this.background = new quad(background);
            t = new text(text);
            this.background.width = t.tex.width;
            this.background.height = t.tex.height;
            this.onClick = onClick;
            button = buttonToCheck;
            Game.buttonMan.Add(this);
        }

        public Button(string text, Texture2D background, String toEcho, MouseButton buttonToCheck)
        {
            this.background = new quad(background);
            t = new text(text);
            this.background.width = t.tex.width;
            this.background.height = t.tex.height;
            this.echo = toEcho;
            onClick = Echo;
            button = buttonToCheck;
            Game.buttonMan.Add(this);
        }

        private void Echo()
        {
            Console.WriteLine(echo);
        }

        public void SetPos(Point p)
        {
            t.pos.xPos = p.X;
            t.pos.yPos = p.Y;
            background.pos.xPos = p.X;
            background.pos.yPos = p.Y;
        }
    }
}
