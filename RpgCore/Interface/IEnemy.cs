using RpgCore.StateMachine;

namespace RpgCore.Interface
{
    public interface IEnemy : IFighter
    {
        StateMachineSystem StateMachine { get; }
        IState CurrentState { get; }
    }
}
