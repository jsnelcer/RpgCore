using System;
using RpgCore.Enum;
using RpgCore.Interface;

namespace RpgCore.StateMachine
{
    public class Attack : IState
    {
        public StateType Type { get; private set; }
        readonly IFighter owner;
        readonly IFighter target;

        public Attack(IFighter actor, IFighter victim)
        {
            Type = StateType.Attack;
            owner = actor;
            target = victim;
        }

        public void Enter()
        {
#if DEBUG
            Console.WriteLine("Charge!");
#endif
        }

        public void Execute()
        {
            owner.Attack(target);
        }

        public void Exit()
        {
#if DEBUG
            Console.WriteLine("End Attack");
#endif
        }
    }
}
