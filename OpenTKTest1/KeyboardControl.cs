using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Input;

namespace OpenTKTest1
{
    class KeyboardControl : iComponent
    {
        public int speed;

        public KeyboardControl(int speed)
        {
            this.speed = speed;
        }

        public void Run(renderable r)
        {
            if(Game.Keyboard.KeyHeld(Key.W))
            {
                r.pos.yPos -= speed;
            }

            if (Game.Keyboard.KeyHeld(Key.S))
            {
                r.pos.yPos += speed;
            }

            if (Game.Keyboard.KeyHeld(Key.A))
            {
                r.pos.xPos -= speed;
            }

            if (Game.Keyboard.KeyHeld(Key.D))
            {
                r.pos.xPos += speed;
            }
        }
    }
}
