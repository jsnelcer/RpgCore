using RpgCore.Enum;

namespace RpgCore.Interface
{
    public interface IQuest : IRewards
    {
        int Id { get; }
        string Title { get; }
        string Description { get; }

        QuestType Type { get; }

        bool Active { get; }
        bool IsComplete();   

        void Reward(IQuestCompleter character);
        string GetConditions();
        void AcceptQuest(IQuestCompleter character);
        void UpdateQuest(object sender);
    }
}
