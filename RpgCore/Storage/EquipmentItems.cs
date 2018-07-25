using System.Collections.Generic;
using System.Linq;
using RpgCore.Enum;
using RpgCore.Items;

namespace RpgCore.Storaged
{
    public sealed class EquipmentItems : Storage<Equipment>
    {
        //private static EquipmentItems instance = null;

        public EquipmentItems() // Instance
        {
            //get
            //{
            //    if (instance == null)
            //    {
            //        instance = new EquipmentItems();
            //    }
            //    return instance;
            //}
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
            // TODO
            // change equip
        }

        public Equipment GetItemFromSlot(EquipSlot slot)
        {
            return items.Select(x => x).Where(x => x.Slot == slot).FirstOrDefault();
        }
    }
}