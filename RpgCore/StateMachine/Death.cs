using System;
using RpgCore.Inteface;
using RpgCore.Enum;

namespace RpgCore.StateMachine
{
    public class Death : IState
    {
        public StateType Type { get; private set; }

        ICharacter owner;

        public Death(ICharacter actor)
        {
            Type = StateType.Death;
            owner = actor;
        }

        public void Enter()
        {
            Console.WriteLine(owner.Name + " dies");
        }

        public void Execute()
        {
            Console.WriteLine(owner.Name + ": I am Death ");
        }

        public void Exit()
        {
            Console.WriteLine(owner.Name + ": Resurrected ");
        }
    }
}
