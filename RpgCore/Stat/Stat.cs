using System.Collections.Generic;
using System.Linq;
using RpgCore.Enum;
using RpgCore.Inteface;

namespace RpgCore.Stats
{
    public class Stat : IStat
    {
        private List<IEffect> Modifiers { get; set; }

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

            Modifiers = new List<IEffect>();
        }
        
        public void AddModifier(IEffect value)
        {
            Modifiers.Add(value);
        }

        public void RemoveModifier(IEffect value)
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

        public void EquipEffect(EquipEffect effect)
        {
            Modifiers.Add(effect);
        }
    }
}
