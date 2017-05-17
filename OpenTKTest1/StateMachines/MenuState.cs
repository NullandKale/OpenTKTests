using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKTest1.StateMachines
{
    class MenuState : iState
    {
        GameState gState;

        public MenuState()
        {
            gState = GameStateManager.man.gState;
        }

        public void enter()
        {
            Console.WriteLine("Entered MenuState");
            Console.WriteLine("Press g to enter GameState");
            //initialize Objects nessasary for displaying menu and set man.currentState = this;
        }

        public void update()
        {
            checkStates();
            if(Game.input.KeyFallingEdge(OpenTK.Input.Key.G))
            {
                toGameState();
            }
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

        private void checkStates()
        {
            if (gState == null)
            {
                gState = GameStateManager.man.gState;
            }
        }
    }
}
