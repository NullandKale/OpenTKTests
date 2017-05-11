using OpenTK;
using OpenTK.Input;

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

        public bool KeyHeld(Key k)
        {
            if (!isKeystateValid())
            {
                return false;
            }
            else
            {
                return lastKeyState.IsKeyDown(k) && keyState.IsKeyDown(k);
            }
        }

        private bool isKeystateValid()
        {
            return keyState != null && lastKeyState != null;
        }
    }
}
