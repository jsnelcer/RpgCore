using System.Collections.Generic;
using System.Linq;
using RpgCore.Enum;
using RpgCore.Interface;

namespace RpgCore.Stats
{
    public class Stat : IStat
    {
        private List<IEffect> modifiers { get; set; }

        private float value { get; set; }
        private StatType type { get; set; }

        public StatType Type => type;
        public float Value
        {
            get
            {
                return value + modifiers.Sum(x=>x.Value);
            }
        }
        
        List<IEffect> IStat.Modifiers => this.modifiers;

        public Stat(float value, StatType type)
        {
            this.value = value;
            this.type = type;

            modifiers = new List<IEffect>();
        }
        
        public void AddModifier(IEffect value)
        {
            modifiers.Add(value);
        }

        public void RemoveModifier(IEffect value)
        {
            modifiers.Remove(value);
        }

        public void RemoveEquipEffects()
        {
            modifiers.RemoveAll(x => x.GetType() == typeof(EquipEffect));
        }

        //maybe change?....
        public void ApplyInstantEffect(InstantEffect effect)
        {
            value += effect.Value;
        }

        public void DurationEffectStep(TimeEffect effect)
        {
            if (effect.Stack > 0)
            {
                AddModifier(effect);
            }
            effect.Used(); //init stack -1;
        }

        public void DurationEffectEnd(TimeEffect effect)
        {
            modifiers.RemoveAll(x => x == effect);
        }

        public void EquipEffect(EquipEffect effect)
        {
            modifiers.Add(effect);
        }
    }
}
