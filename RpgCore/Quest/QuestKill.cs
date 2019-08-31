using RpgCore.Enum;
using RpgCore.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RpgCore.Quest
{
    public class QuestKill : Quest
    {
        private int Goal { get; set; }
        private int Current { get; set; }
        private IEnemy Target { get; set; }

        public QuestKill(
            int id, string title, string description, 
            List<IItem> items, List<IStat> stats,
            IEnemy target, int goal
            ): base(QuestType.Kill, id, title, description, items, stats)
        {
            Target = target;
            Current = 0;
            Goal = goal;
        }

        public override string GetConditions()
        {
            return $"Kill {Target.Name}: {Current}/{Goal}";
        }

        public override bool IsComplete()
        {
            return Current >= Goal;
        }

        public override void AcceptQuest(IFighter character)
        {
            base.AcceptQuest(character);
        }

        public override void UpdateQuest(object sender)
        {
            IFighter enemy = (IFighter)sender;
            if (enemy.Name == Target.Name && enemy.Description == Target.Description)
            {
                Current++;
            }
        }
    }
}
