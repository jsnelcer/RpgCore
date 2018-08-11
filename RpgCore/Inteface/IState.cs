using RpgCore.Enum;

namespace RpgCore.Inteface
{
    public interface IState
    {
        StateType Type { get; }

        void Enter();

        void Execute();

        void Exit();
    }

}
