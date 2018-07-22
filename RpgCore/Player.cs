using System;
using System.Collections.Generic;
using System.Linq;
using RpgCore.Storaged;
using RpgCore.Stats;
using RpgCore.Items;
using RpgCore.Enum;
using RpgCore.Inteface;

namespace RpgCore
{
    public class Player : Character
    {
        public Inventory Inventory { get; private set; }
        public QuickUse QuickUse { get; private set; }
        public EquipmentItems Equip { get; private set; }

        private StatsManager statsManager;


        public delegate void EquipChangeEvent();
        public static event EquipChangeEvent EquipChange;

        public Player(string name, string description, List<Stat> baseStats)
            :base(name, description)
        {
            statsManager = new StatsManager(baseStats);

            Inventory = new Inventory();
            QuickUse = new QuickUse();
            Equip = new EquipmentItems();

            EquipChange += UpdateStatsFromEquip;
        }

        private void UpdateStatsFromEquip() => statsManager.EquipStats(Equip.GetItems());

        public void UseItem(IUseable item) => AddEffect(item.Use());

        public void Interact(IInteractable item) => item.Interact();

        public void PickUp(Item item) => Inventory.AddItem(item);

        public void EquipItem(Equipment item)
        {
            try
            {
                Inventory.RemoveItem(item);
                if (Equip.GetItems().Any(x => x.Slot == item.Slot))
                {
                    Equipment change = Equip.GetItemFromSlot(item.Slot);
                    FromEquipToInventory(change);
                }

                Equip.AddItem(item);
                if (EquipChange != null)
                {
                    EquipChange.Invoke();
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public void FromEquipToInventory(Equipment item)
        {
            try
            {
                Equip.RemoveItem(item);
            }
            catch (Exception e)
            {
                throw (e);
            }
            Inventory.AddItem(item);
        }
        
        public void AddEffect(Effect effect)
        {
            if(effect.Target == EffectTarget.Character)
            {
                statsManager.ApplyEffect((IEffect<StatsManager>)effect);    
            }
            if (effect.Target == EffectTarget.Equip)
            {
                Equip.ApplyEffect((IEffect<Equipment>)effect);
            }
        } 

        public void Update() => statsManager.UpdateStats();

        public Stat GetStat(StatType type) => statsManager.GetStat(type);
    }
}
