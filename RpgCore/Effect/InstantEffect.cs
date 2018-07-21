using RpgCore.Enum;
using RpgCore.Inteface;
using System.Linq;

namespace RpgCore
{
    public class InstantEffect : Effect, IEffect<StatsManager>
    {
        public InstantEffect(EffectTarget target, StatType targetStat, float value)
            :base(target, targetStat, value)
        {
            
        }

        public void ApplyEffect(StatsManager target)
        {
            base.ApplyEffect(target.Stats.Where(x => x.Type == this.GetTargetStat()).FirstOrDefault());
        }
    }
}
