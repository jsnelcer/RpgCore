using RpgCore.Interface;
using RpgCore.Enum;
using System.Collections.Generic;
using System.Linq;

namespace RpgCore.Stats
{
    public class RegenerationStat : IStat
    {
        private List<IEffect> modifiers { get; set; }

        private float value { get; set; }
        private StatType type { get; set; }
        private float maxValue
        {
            get;
            set;
        }
        
        public StatType Type => type;
        public float MaxValue
        {
            get
            {
                return this.maxValue + modifiers.Sum(x => x.Value);
            }
        }
        public float Value => value;

        public List<IEffect> Modifiers => this.modifiers;

        public RegenerationStat(float maxValue, StatType type)
        {

            this.modifiers = new List<IEffect>();
            this.maxValue = maxValue;
            this.value = maxValue;
            this.type = type;
        }

        public void ApplyInstantEffect(InstantEffect effect)
        {
            if(value + effect.Value < MaxValue)
            {
                value += effect.Value;
            }
            else
            {
                this.value = MaxValue;
            }
        }

        public void DurationEffectStep(TimeEffect effect)
        {
            if (effect.Stack > 0)
            {
                if (value + effect.Value < MaxValue)
                {
                    value += effect.Value;
                }
                else
                {
                    this.value = MaxValue;
                }
            }
            effect.Used(); //init stack -1;
        }

        public void DurationEffectEnd(TimeEffect effect)
        {
            // do nothing
        }

        public void EquipEffect(EquipEffect effect)
        {
            modifiers.Add(effect);
            if (value > MaxValue) { value = MaxValue; }
        }

        public void RemoveEquipEffects()
        {
            modifiers.RemoveAll(x => x.GetType() == typeof(EquipEffect));
        }
    }
}
