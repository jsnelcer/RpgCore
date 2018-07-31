using RpgCore.Enum;
using RpgCore.Inteface;
using System.Linq;
using System.Collections.Generic;

namespace RpgCore
{
    public class InstantEffect : IEffect<StatsManager>
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

        public void ApplyEffect(StatsManager target)
        {
            target.Stats.Where(x => x.Type == targetStat).FirstOrDefault().ApplyInstantEffect(this);
        }

        public void IncreastValue(float _value)
        {
            value += _value;
        }
    }
}
