using RpgCore;
using RpgCore.Enum;
using System.Linq;
using RpgCore.Inteface;

namespace RpgCore
{
    public class TimeEffect : IEffect, IRefill
    {
        private int initStack { get; set; }
        private EffectTarget target { get; set; }
        private float value { get; set; }
        private StatType targetStat { get; set; }

        public EffectTarget Target => target;
        public StatType TargetStat => targetStat;
        public float Value => value;

        public int Stack { get; private set; }
        public int Step { get; private set; }


        public TimeEffect(EffectTarget _target, StatType _targetStat, float _value, int stack, int step = 1)
        {
            this.target = _target;
            this.targetStat = _targetStat;
            this.value = _value;
            this.Stack = stack;
            this.initStack = stack;
            this.Step = step;
        }
        
        public void AddStack(int stack)
        {
            this.Stack += stack;
        }

        public void Used()
        {
            this.Stack -= Step;
        }

        public void ApplyEffect(IStat target)
        {
            target.DurationEffectStep(this);
        }
        
        public void IncreastValue(float _value)
        {
            value += _value;
        }

        public void Refill()
        {
            Stack = initStack;
        }
    }
}
