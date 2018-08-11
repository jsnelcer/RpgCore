using RpgCore.StateMachine;

namespace RpgCore.Inteface
{
    public interface IEnemy : IFighter, INpc
    {
        StateMachineSystem StateMachine { get; }
        IState CurrentState { get; }
    }
}
