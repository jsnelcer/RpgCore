using RpgCore.Enum;
using RpgCore.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RpgCore.Quest
{
    public class QuestGather : Quest
    {
        private int Goal { get; set; }
        private int Current { get; set; }
        private IItem Target { get; set; }

        public QuestGather(
            int id, string title, string description,
            List<IItem> items, List<IStat> stats,
            IItem target, int goal
            ) : base(QuestType.Gather, id, title, description, items, stats)
        {
            Target = target;
            Current = 0;
            Goal = goal;
        }

        public override string GetConditions()
        {
            return $"Find {Target.Name}: {Current}/{Goal}";
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
            IItem item = (IItem)sender;
            if (item.Id == Target.Id)
            {
                Current++;
            }
        }
    }
}
