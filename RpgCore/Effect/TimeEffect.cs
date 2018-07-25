using RpgCore;
using RpgCore.Enum;
using RpgCore.Stats;
using System;
namespace RpgCore
{
    public class TimeEffect : Effect
    {
        public int Stack { get; private set; }

        public TimeEffect(EffectTarget target, StatType targetStat, float value, int stack)
            :base(target, targetStat, value)
        {
            this.Stack = stack;
        }
        
        public void AddStack(int stack)
        {
            this.Stack += stack;
        }

        public void Used()
        {
            this.Stack--;
        }
    }
}
