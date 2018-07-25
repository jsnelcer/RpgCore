using RpgCore.Enum;
using RpgCore.Inteface;
using System.Linq;
using RpgCore.Storaged;

namespace RpgCore
{
    public class EquipEffect : Effect, IEffect<EquipmentItems>
    {
        public EquipEffect(EffectTarget target, StatType targetStat, float value)
            :base(target, targetStat, value)
        {
        }
        
        public void ApplyEffect(EquipmentItems equip)
        {
            //equip.Items.Where(x => x == Target).FirstOrDefault().AddEquipEffect(this);
        }
    }
}
