using RpgCore.Inteface;
using RpgCore.Enum;
using System.Collections.Generic;
using System.Linq;

namespace RpgCore.Stats
{
    public class RegenerationStat : IStat
    {
        private List<IEffect> Modifiers { get; set; }

        private float value { get; set; }
        private StatType type { get; set; }
        public float MaxValue
        {
            get
            {
                return value + Modifiers.Sum(x => x.Value);
            }
            private set
            {
                MaxValue = value;
            }
        }
        
        public StatType Type => type;

        public float Value => value;

        public RegenerationStat(float maxValue, StatType type)
        {
            this.MaxValue = maxValue;
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
            if (value + effect.Value < MaxValue)
            {
                value += effect.Value;
            }
            else
            {
                this.value = MaxValue;
            }
        }

        public void DurationEffectEnd(TimeEffect effect)
        {
            // do nothing
        }

        public void EquipEffect(EquipEffect effect)
        {
            Modifiers.RemoveAll(x => x == effect);
        }
    }
}
