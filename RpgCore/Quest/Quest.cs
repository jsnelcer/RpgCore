using RpgCore.Enum;
using RpgCore.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RpgCore.Quest
{
    public class Quest : IQuest
    {
        public QuestType Type { get; set; }

        public bool Active { get; set; }

        public List<IItem> Items { get; set; }

        public List<IStat> Stats { get; set; }

        public bool IsComplete()
        {
            return false;
        }

        public void Reward(ICharacter character)
        {
            Items.ForEach (item => {
                character.AddToInventory(item);
            });

            Stats.ForEach(stat =>
            {
                character.UpgradeStat(stat);
            });
        }
    }
}
