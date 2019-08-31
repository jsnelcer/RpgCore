using RpgCore.Enum;

namespace RpgCore.Interface
{
    public interface IState
    {
        StateType Type { get; }

        void Enter();

        void Execute();

        void Exit();
    }
}
