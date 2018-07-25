using System;
using System.Collections.Generic;
using System.Linq;
using RpgCore.Enum;
using RpgCore.Items;

namespace RpgCore.Storaged
{
    public class StorageManager
    {
        public static Inventory inventory { get; private set; }
        public static QuickUse quickUse { get; private set; }
        public static EquipmentItems equip { get; private set; }

        private static StorageManager instance = null;

        public static StorageManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StorageManager();
                    inventory = new Inventory(); //.Instance;
                    quickUse = new QuickUse(); //.Instance;
                    equip = new EquipmentItems(); //.Instance;
                }
                return instance;
            }
        }

        public event EquipChangeEvent EquipChange;
        public delegate void EquipChangeEvent();


        internal void ApplyEffect(Effect effect)
        {
            var @switch = new Dictionary<Type, Action> {
                { typeof(Effect), () => {
                                        } },
                { typeof(TimeEffect), () => ApplyEffect((TimeEffect)effect) },
                { typeof(InstantEffect), () => ApplyEffect((InstantEffect)effect) },
            };

            @switch[effect.GetType()]();
        }

        public void FromInventory2QuickUse(ConsumableItem item)
        {
            try
            {
                inventory.RemoveItem(item);
            }
            catch (Exception e)
            {
                throw (e);
            }
            quickUse.AddItem(item);
        }

        public void FromQuickUse2Inventory(ConsumableItem item)
        {
            try
            {
                quickUse.RemoveItem(item);
            }
            catch (Exception e)
            {
                throw (e);
            }
            inventory.AddItem(item);
        }



        public void FromInventory2Equip(Equipment item)
        {
            try
            {
                inventory.RemoveItem(item);
            }
            catch (Exception e)
            {
                throw (e);
            }
            if(equip.GetItems().Any(x=>x.Slot == item.Slot))
            {
                Equipment change = equip.GetItemFromSlot(item.Slot);
                FromEquip2Inventory(change);
            }

            equip.AddItem(item);
            if(EquipChange != null)
            {
                EquipChange.Invoke();
            }
        }
        
        public void FromEquip2Inventory(Equipment item)
        {
            try
            {
                equip.RemoveItem(item);
            }
            catch (Exception e)
            {
                throw (e);
            }
            inventory.AddItem(item);
        }

    }
}
