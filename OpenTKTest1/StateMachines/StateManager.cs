using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTKTest1
{
    abstract class StateManager
    {
        public List<iState> states;
        public iState CurrentState;

        public abstract void update();
    }
}
