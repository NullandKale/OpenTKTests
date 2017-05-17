using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKTest1.StateMachines
{
    class PauseState : iState
    {
        GameState gState;
        MenuState mState;

        public PauseState()
        {
            gState = GameStateManager.man.gState;
            mState = GameStateManager.man.mState;
        }

        public void enter()
        {
            Console.WriteLine("Entered PauseState");
            Console.WriteLine("Press G to enter GameState");
            Console.WriteLine("Press M to enter MenuState");
            //init Objects
        }

        public void update()
        {
            checkStates();
            if(Game.input.KeyFallingEdge(OpenTK.Input.Key.G))
            {
                toGameState();
            }

            if (Game.input.KeyFallingEdge(OpenTK.Input.Key.M))
            {
                toMenuState();
            }
            //Update objects
        }

        public void clean()
        {
            //clean up openGL objects
        }

        private void toGameState()
        {
            Console.WriteLine("Changing to GameState");
            GameStateManager.man.CurrentState = gState;
            gState.enter();
            clean();
        }

        private void toMenuState()
        {
            Console.WriteLine("Changing to MenuState");
            GameStateManager.man.CurrentState = mState;
            mState.enter();
            clean();
        }

        private void checkStates()
        {
            if(gState == null)
            {
                gState = GameStateManager.man.gState;
            }
            if(mState == null)
            {
                mState = GameStateManager.man.mState;
            }
        }
    }
}
