using RpgCore.StateMachine;

namespace RpgCore.Interface
{
    public interface IEnemy : IFighter, INpc
    {
        StateMachineSystem StateMachine { get; }
        IState CurrentState { get; }
    }
}
