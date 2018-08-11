using System;
using System.Linq;
using System.Collections.Generic;
using RpgCore.Inteface;
using RpgCore.Enum;

namespace RpgCore.StateMachine
{
    public class Idle : IState
    {
        public StateType Type { get; private set; }
        ICharacter owner;

        public Idle(ICharacter actor)
        {
            owner = actor;
            Type = StateType.Idle;
        }

        public void Enter()
        {
            Console.WriteLine(owner.Name + ": idle start");
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
                Console.WriteLine(owner.Name + " see nothing");
            }
        }

        public void Exit()
        {
            Console.WriteLine(owner.Name + ": idle end");
        }
    }
}
