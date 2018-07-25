using RpgCore.Enum;

namespace RpgCore
{
    public class EquipEffect : Effect
    {
        public EquipEffect(EffectTarget target, StatType targetStat, float value)
            :base(target, targetStat, value)
        {

        }
    }
}
