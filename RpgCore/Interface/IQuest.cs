using RpgCore.Enum;

namespace RpgCore.Interface
{
    public interface IQuest : IRewards
    {
        QuestType Type { get; }

        bool Active { get; }
        bool IsComplete();

        void Reward(ICharacter character);
    }
}
