using RpgCore.Enum;

namespace RpgCore
{
    public class InstantEffect : Effect
    {
        public InstantEffect(EffectTarget target, StatType targetStat, float value)
            :base(target, targetStat, value)
        {
            
        }
    }
}
