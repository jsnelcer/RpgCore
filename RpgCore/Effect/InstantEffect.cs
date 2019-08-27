using RpgCore.Enum;
using RpgCore.Interface;

namespace RpgCore
{
    public class InstantEffect : IEffect
    {
        private EffectTarget target { get; set; }

        private float value { get; set; }

        private StatType targetStat { get; set; }

        public EffectTarget Target => target;

        public StatType TargetStat => targetStat;

        public float Value => value;

        public InstantEffect(EffectTarget _target, StatType _targetStat, float _value)
        {
            this.target = _target;
            this.targetStat = _targetStat;
            this.value = _value;
        }

        protected InstantEffect(InstantEffect anotherEffect)
        {
            this.target = anotherEffect.Target;
            this.targetStat = anotherEffect.TargetStat;
            this.value = anotherEffect.Value;
        }

        public void ApplyEffect(IStat target)
        {
            target.ApplyInstantEffect(this);
        }

        public void IncreastValue(float _value)
        {
            value += _value;
        }

        public IEffect Clone()
        {
            return new InstantEffect(this);
        }
    }
}
