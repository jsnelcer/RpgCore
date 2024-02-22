using System;
using RpgCore.Interface;
using RpgCore.Enum;

namespace RpgCore.StateMachine
{
    public class Death : IState
    {
        public StateType Type { get; private set; }

        readonly ICharacter owner;

        public Death(ICharacter actor)
        {
            Type = StateType.Death;
            owner = actor;
        }

        public void Enter()
        {
#if DEBUG
            Console.WriteLine(owner.Name + " dies");
#endif
        }

        public void Execute()
        {
#if DEBUG
            Console.WriteLine(owner.Name + ": I am Death ");
#endif
        }

        public void Exit()
        {
#if DEBUG
            Console.WriteLine(owner.Name + ": Resurrected ");
#endif
        }
    }
}
