using System;
using System.Collections.Generic;
using System.Linq;
using RpgCore.Enum;
using RpgCore.Inteface;
using RpgCore.Items;

namespace RpgCore.Storaged
{
    public sealed class EquipmentItems : Storage<Equipment>
    {
       

        public EquipmentItems() 
        {
        }

        public override void AddItem(Equipment item)
        {
            if(items == null)
            {
                base.AddItem(item);
            }
            else if (!items.Exists(x => x.Slot == item.Slot))
            {
                base.AddItem(item);
            }
        }

        public Equipment GetItemFromSlot(EquipSlot slot)
        {
            return items.Select(x => x).Where(x => x.Slot == slot).FirstOrDefault();
        }

        internal void ApplyEffect(IEffect<Equipment> effect)
        {
            throw new NotImplementedException();
        }
    }
}