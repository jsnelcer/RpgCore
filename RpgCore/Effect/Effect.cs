using RpgCore.Stats;
using RpgCore.Inteface;
using RpgCore.Enum;
using RpgCore.Items;

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
        
        internal void IncreastValue(float value) => Value += value;

        public void ApplyEffect(Stat target) => target.AddModifier(this);

        public StatType GetTargetStat()
        {
            return TargetStat;
        }
    }
}
