using RpgCore.Stats;
using RpgCore.Enum;
using System;
namespace RpgCore.Stats
{
    public class RegenerationStat : Stat
    {
        public float MaxValue { get; private set; }
        public RegenerationStat(float maxValue, StatType type)
            :base(maxValue, type)
        {
            this.MaxValue = maxValue;
        }

        public override void ChangeBaseValue(float value)
        {
            if (this.Value + value <= 0)
            {
                this.Value = 0;
            }
            else if (this.Value + value >= this.MaxValue)
            {
                this.Value = this.MaxValue;
            }
            else
            {
                this.Value += value;
            }
        }

        public override void ChangeBaseValue(Effect value)
        {
            ChangeBaseValue(value.Value);
        }
    }
}
