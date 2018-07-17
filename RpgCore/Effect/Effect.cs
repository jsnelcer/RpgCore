using System;
using RpgCore.Enum;

namespace RpgCore
{
    public class Effect
    {
        public EffectTarget Target { get; private set; }
        public StatType TargetStat { get; private set; }
        public float Value { get; private set; }

        public Effect(EffectTarget target, StatType targetStat, float value)
        {
            this.Target = target;
            this.TargetStat = targetStat;
            this.Value = value;
        }

        internal void IncreastValue(float value)
        {
            Value += value;
        }
    }
}
