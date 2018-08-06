using RpgCore.Enum;
using RpgCore.Inteface;

namespace RpgCore
{
    public class EquipEffect : IEffect
    {
        private EffectTarget target { get; set; }

        private float value { get; set; }

        private StatType targetStat { get; set; }

        public EffectTarget Target => target;

        public StatType TargetStat => targetStat;

        public float Value => value;

        public EquipEffect(EffectTarget _target, StatType _targetStat, float _value)
        {
            this.target = _target;
            this.targetStat = _targetStat;
            this.value = _value;
        }

        protected EquipEffect(EquipEffect anotherEffect)
        {
            this.target = anotherEffect.Target;
            this.targetStat = anotherEffect.TargetStat;
            this.value = anotherEffect.Value;
        }

        public void ApplyEffect(IStat target)
        {
            target.EquipEffect(this);
        }

        public void IncreastValue(float _value)
        {
            value += _value;
        }

        public IEffect Clone()
        {
            return new EquipEffect(this);
        }
    }
}
