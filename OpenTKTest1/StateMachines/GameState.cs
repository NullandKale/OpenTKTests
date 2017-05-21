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
        List<Action> updaters;

        public Button goodFPS;
        public Button badFPS;

        public GameState()
        {
            pState = GameStateManager.man.pState;
            updaters = new List<Action>();

            goodFPS = new Button("FPS - Good", Game.buttonBackground, "", OpenTK.Input.MouseButton.Left);
            updaters.Add(goodFPS.update);

            badFPS = new Button("FPS - Bad", Game.buttonBackground, "", OpenTK.Input.MouseButton.Left);
            updaters.Add(badFPS.update);
        }

        public void enter()
        {
            Console.WriteLine("Entered GameState");
        }

        public void update()
        {
            checkStates();
            if(Game.input.KeyFallingEdge(OpenTK.Input.Key.Escape))
            {
                toPauseState();
            }

            for (int i = 0; i < updaters.Count; i++)
            {
                updaters[i].Invoke();
            }

            if(Game.frameTime <= 18)
            {
                goodFPS.SetActive(true);
                badFPS.SetActive(false);
            }
            else
            {
                if(Game.tick == 15)
                {
                    Console.WriteLine(Game.frameTime);
                }
                goodFPS.SetActive(false);
                badFPS.SetActive(true);
            }
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
