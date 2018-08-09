using System;
using System.Collections.Generic;
using System.Text;

namespace RpgCore.Inteface
{
    public interface IState
    {
        void Enter();

        void Execute();

        void Exit();
    }

}
