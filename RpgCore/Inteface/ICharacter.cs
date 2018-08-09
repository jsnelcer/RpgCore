using RpgCore.StateMachine;

namespace RpgCore.Inteface
{
    public interface ICharacter : IEntity
    {
        IState CurrentState { get; }
    }
}
