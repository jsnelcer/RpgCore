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

        public void ApplyInstantEffect(float value)
        {
            if(value + GetBaseValue() < MaxValue)
            {
                ChangeBaseValue(value);
            }
            else{
                this.Value = MaxValue;
            }
            
            
        }
    }
}
