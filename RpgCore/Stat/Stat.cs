using System.Collections.Generic;
using System.Linq;
using RpgCore.Enum;
using RpgCore;

namespace RpgCore.Stats
{
    public class Stat : BaseStat
    {
        private List<Effect> Modifiers { get; set; }
        public Stat(float baseValue, StatType type):
            base(baseValue, type)
        {
            Modifiers = new List<Effect>();
        }

        public void ChangeBaseValue(float value)
        {
            this.Value += value;
        }

        public void AddModifier(Effect value)
        {
            Modifiers.Add(value);
        }

        public void RemoveModifier(Effect value)
        {
            Modifiers.Remove(value);
        }

        public override float GetValue()
        {
            return base.Value + Modifiers.Select(x=>x.Value).Sum();
        }

        public float GetBaseValue()
        {
            return base.Value;
        }

        public void RemoveEquipModifier()
        {
            Modifiers.RemoveAll(x => x.GetType() == typeof(EquipEffect));
        }
    }
}
