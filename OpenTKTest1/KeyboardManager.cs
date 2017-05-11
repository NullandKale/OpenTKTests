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
    class KeyboardManager
    {
        private KeyboardState lastKeyState;
        private KeyboardState keyState;

        public KeyboardManager()
        {
            Game.window.UpdateFrame += update;
        }

        public void update(object sender, FrameEventArgs e)
        {
            if(keyState != null)
            {
                lastKeyState = keyState;
            }

            keyState = Keyboard.GetState();

        }

        public bool KeyRisingEdge(Key k)
        {
            if(!isKeystateValid())
            {
                return false;
            }
            else
            {
                return lastKeyState.IsKeyDown(k) && keyState.IsKeyUp(k);
            }
        }

        public bool KeyFallingEdge(Key k)
        {
            if (!isKeystateValid())
            {
                return false;
            }
            else
            {
                return lastKeyState.IsKeyUp(k) && keyState.IsKeyDown(k);
            }
        }

        private bool isKeystateValid()
        {
            return keyState != null || lastKeyState != null;
        }
    }
}
