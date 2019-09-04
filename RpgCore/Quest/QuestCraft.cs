using RpgCore.Crafting;
using RpgCore.Enum;
using RpgCore.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RpgCore.Quest
{
    public class QuestCraft : Quest
    {
        private int Goal { get; set; }
        private int Current { get; set; }
        private IItem Target { get; set; }

        public QuestCraft(
            int id, string title, string description,
            List<IItem> items, List<IStat> stats,
            IItem target, int goal
            ) : base(QuestType.Craft, id, title, description, items, stats)
        {
            Target = target;
            Current = 0;
            Goal = goal;
        }

        public override string GetConditions()
        {
            return $"Craft {Target.Name}: {Current}/{Goal}";
        }

        public override bool IsComplete()
        {
            return Current >= Goal;
        }

        public override void AcceptQuest(IFighter character)
        {
            base.AcceptQuest(character);
            Current = character.GetInventory().FindAll(item => item.Id == Target.Id).Count;
        }

        public override void UpdateQuest(object sender)
        {
            Receipt receipt = (Receipt)sender;
            if (receipt.Result.Id == Target.Id)
            {
                Current++;
            }
        }
    }
}
