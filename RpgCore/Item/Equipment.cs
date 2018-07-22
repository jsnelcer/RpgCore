using System.Collections.Generic;
using System.Linq;
using RpgCore.Enum;

namespace RpgCore.Items
{
    public class Equipment : Item
    {
        public EquipSlot Slot { get; private set; }
        public List<EquipEffect> EquipEffects { get; private set; }

        public Equipment(int id, string name, string description, EquipSlot slot)
            :base(id, name, description)
        {
            this.Slot = slot;
            EquipEffects = new List<EquipEffect>();
        }

        internal Equipment(int id, string name, string description)
            : base(id, name, description)
        {
            EquipEffects = new List<EquipEffect>();
        }

        public void SetSlot(EquipSlot slot)
        {
            if (Slot == 0)
            {
                this.Slot = slot;
            }
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
            EquipEffects.Find(x => x.TargetStat == effect.TargetStat).IncreastValue(-effect.Value);
            EquipEffects.RemoveAll(x => x.Value == 0);
        }
    }
}
