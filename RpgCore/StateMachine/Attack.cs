using System;
using RpgCore.Enum;
using RpgCore.Interface;

namespace RpgCore.StateMachine
{
    public class Attack : IState
    {
        public StateType Type { get; private set; }
        IFighter owner;
        IFighter target;

        public Attack(IFighter actor, IFighter victim)
        {
            Type = StateType.Attack;
            owner = actor;
            target = victim;
        }

        public void Enter()
        {
#if Debug
            Console.WriteLine("Charge!");
#endif
        }

        public void Execute()
        {
            owner.Attack(target);
        }

        public void Exit()
        {
#if Debug
            Console.WriteLine("End Attack");
#endif
        }
    }
}
