using RpgCore.Inteface;
using RpgCore.Enum;

namespace RpgCore.Stats
{
    public class RegenerationStat : IStat
    {
        private float value { get; set; }
        private StatType type { get; set; }
        public float MaxValue { get; private set; }
        
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
    }
}
