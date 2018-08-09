using System;
using System.Collections.Generic;
using RpgCore.Inteface;

namespace RpgCore.StateMachine
{
    public class Attack : IState
    {
        IFighter owner;
        IFighter target;

        public Attack(IFighter actor, IFighter victim)
        {
            owner = actor;
            victim = target;
        }

        public void Enter()
        {
        }

        public void Execute()
        {
            owner.Attack(target);
        }

        public void Exit()
        {
            
        }
    }
}
