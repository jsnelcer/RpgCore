using System;
using System.Linq;
using System.Collections.Generic;
using RpgCore.Interface;
using RpgCore.Enum;

namespace RpgCore.StateMachine
{
    public class Idle : IState
    {
        public StateType Type { get; private set; }
        readonly ICharacter owner;

        public Idle(ICharacter actor)
        {
            owner = actor;
            Type = StateType.Idle;
        }

        public void Enter()
        {
#if DEBUG
            Console.WriteLine(owner.Name + ": idle start");
#endif
        }

        public void Execute()
        {
            List<IInteractable> entits = owner.LookAround().Where(x => x is IInteractable).Select(x => x as IInteractable).ToList();
            if (entits.Any())
            {
                IInteractable entity = entits.First();
                entity.Interact(this as ICharacter);
            }
            else
            {
#if DEBUG
                Console.WriteLine(owner.Name + " see nothing");
#endif
            }
        }

        public void Exit()
        {
#if DEBUG
            Console.WriteLine(owner.Name + ": idle end");
#endif
        }
    }
}
