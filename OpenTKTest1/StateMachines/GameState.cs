using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKTest1.StateMachines
{
    class GameState : iState
    {
        PauseState pState;

        public GameState()
        {
            pState = GameStateManager.man.pState;
        }

        public void enter()
        {
            Console.WriteLine("Entered GameState");
            Console.WriteLine("Press P to enter pauseState");
            //Init Objects
        }

        public void update()
        {
            checkStates();
            if(Game.input.KeyFallingEdge(OpenTK.Input.Key.P))
            {
                toPauseState();
            }
            //Update Objects
        }

        public void clean()
        {
            //clean up openGL objects
        }

        private void toPauseState()
        {
            Console.WriteLine("Changing to PauseState");
            GameStateManager.man.CurrentState = pState;
            pState.enter();
            clean();
        }

        private void checkStates()
        {
            if (pState == null)
            {
                pState = GameStateManager.man.pState;
            }
        }
    }
}
