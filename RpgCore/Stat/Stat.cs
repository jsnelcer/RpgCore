using System.Collections.Generic;
using System.Linq;
using RpgCore.Enum;
using RpgCore.Inteface;

namespace RpgCore.Stats
{
    public class Stat : IStat
    {
        private List<IEffect<StatsManager>> Modifiers { get; set; }

        private float value { get; set; }
        private StatType type { get; set; }

        public StatType Type => type;
        public float Value
        {
            get
            {
                return value + Modifiers.Sum(x=>x.Value);
            }
        }

        public Stat(float value, StatType type)
        {
            this.value = value;
            this.type = type;

            Modifiers = new List<IEffect<StatsManager>>();
        }
        
        public void AddModifier(IEffect<StatsManager> value)
        {
            Modifiers.Add(value);
        }

        public void RemoveModifier(IEffect<StatsManager> value)
        {
            Modifiers.Remove(value);
        }

        public void RemoveEquipModifier()
        {
            Modifiers.RemoveAll(x => x.GetType() == typeof(EquipEffect));
        }

        //maybe change?....
        public void ApplyInstantEffect(InstantEffect effect)
        {
            value += effect.Value;
        }

        public void DurationEffectStep(TimeEffect effect)
        {
            AddModifier(effect);
        }

        public void DurationEffectEnd(TimeEffect effect)
        {
            Modifiers.RemoveAll(x => x == effect);
        }
    }
}
