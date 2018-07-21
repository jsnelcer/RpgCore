using System.Collections.Generic;
using System.Linq;
using RpgCore.Enum;

namespace RpgCore.Items
{
    public class Equipment : Item
    {
        public EquipSlot Slot { get; private set; }
        private List<EquipEffect> EquipEffects { get; set; }

        public Equipment(int id, string name, string description, EquipSlot slot)
            :base(id, name, description)
        {
            this.Slot = slot;
            EquipEffects = new List<EquipEffect>();
        }

        public void AddEquipEffect(EquipEffect effect)
        {
            if (!EquipEffects.Exists(x => x.TargetStat == effect.TargetStat))
            {
                EquipEffects.Add(effect);
            }
            else
            {
                EquipEffects.Find(x => x.TargetStat == effect.TargetStat).IncreastValue(effect.Value);
            }
        }

        public void RemoveEquipEffect(EquipEffect effect)
        {
            EquipEffects.Remove(effect);
        }

        public List<EquipEffect> GetEquipEffects()
        {
            if (EquipEffects.Any())
            {
                return EquipEffects;
            }
            else
            {
                return null;
            }
        }
    }
}
