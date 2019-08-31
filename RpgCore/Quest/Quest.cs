using RpgCore.Enum;
using RpgCore.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RpgCore.Quest
{
    public abstract class Quest : IQuest
    {
        public QuestType Type { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public List<IItem> Items { get; set; }
        public List<IStat> Stats { get; set; }

        public Quest(QuestType type, int id, string title, string description, List<IItem> items, List<IStat> stats)
        {
            Type = type;
            Id = id;
            Title = title;
            Description = description;

            Items = items;
            Stats = stats;

            Active = false;
        }

        public abstract bool IsComplete();

        public abstract string GetConditions();

        public virtual void Reward(IQuestCompleter character)
        {
            Items.ForEach (item => {
                character.AddToInventory(item);
            });

            Stats.ForEach(stat =>
            {
                character.UpgradeStat(stat);
            });
        }
        
        public virtual void AcceptQuest(IQuestCompleter character)
        {
            Active = true;
            character.AddQuest(this);
        }

        public abstract void UpdateQuest(object sender);
    }
}
