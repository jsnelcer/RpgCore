using RpgCore.Enum;
using RpgCore.Inteface;
using System.Linq;
using System.Collections.Generic;

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

        public void ApplyEffect(IStat target)
        {
            target.ApplyInstantEffect(this);
        }

        public void IncreastValue(float _value)
        {
            value += _value;
        }
    }
}
