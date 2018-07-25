using RpgCore.Inteface;
using RpgCore.Enum;
using System.Linq;

namespace RpgCore
{
    public class TimeEffect : Effect, IEffect<StatsManager>
    {
        private int Step { get; set; }
        public int Stack { get; private set; }

        public TimeEffect(EffectTarget target, StatType targetStat, float value, int stack, int step = 1)
            :base(target, targetStat, value)
        {
            this.Step = step;
            this.Stack = stack;
        }

        public void AddStack(int stack) => this.Stack += stack;

        public void Used() => this.Stack -= Step;

        public void IncreasteStep(int value) => Step = value + Step <= 0 ? 1 : value + Step;

        public void ApplyEffect(StatsManager target)
        {
            if (target.DurationEffect.Any(x => x == this))
            {
                target.DurationEffect.Find(x => x == this).AddStack(this.Stack);
            }
            else
            {
                target.DurationEffect.Add(this);
            }
        }
    }
}
