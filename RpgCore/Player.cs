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
    public class Player : ICharacter
    {
        private string description { get; set; }
        private string name { get; set; }

        public string Name => name;
        public string Description => description;

        public IStorage<IItem> Inventory { get; private set; }
        public IStorage<ConsumableItem> QuickUse { get; private set; }
        public IStorage<IEquiped> Equip { get; private set; }

        private StatsManager StatsManager;


        public delegate void EquipChangeEvent();
        public static event EquipChangeEvent EquipChange;

        public Player(string name, string description, List<IStat> baseStats, IStorage<IItem> inventory, IStorage<ConsumableItem> quickUse, IStorage<IEquiped> equip)
        {
            this.name = name;
            this.description = description;

            StatsManager = new StatsManager(baseStats);

            this.Inventory = inventory;
            this.QuickUse = quickUse;
            this.Equip = equip;

            EquipChange += UpdateStatsFromEquip;
        }

        private void UpdateStatsFromEquip() => StatsManager.EquipStats(Equip.Items);

        public void UseItem(IUseable item)
        {
            IEffect eff = item.Use();
        }
        
        public void Interact(IInteractable item) => item.Interact();

        public void PickUp(IItem item) => Inventory.AddItem(item);
        
        public void EquipItem(IEquiped item)
        {
            try
            {
                Inventory.RemoveItem(item);
                if (Equip.Items.Any(x => x.Slot == item.Slot))
                {
                    IEquiped change = Equip.Items.Where(x => x.Slot == item.Slot).FirstOrDefault();
                    FromEquipToInventory(change);
                }
                Equip.AddItem(item);
                item.Equiped = true;
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
        
        public void FromEquipToInventory(IEquiped item)
        {
            try
            {
                item.Equiped = false;
                Equip.RemoveItem(item);
            }
            catch (Exception e)
            {
                throw (e);
            }
            Inventory.AddItem(item);
        }
        
        public void AddEffect(IEffect effect) =>  StatsManager.ApplyEffect(effect);

        //public void AddEffect(IEffect<EquipmentItems> effect) => equip.ApplyEffect(effect);

        public void Update() => StatsManager.UpdateStats();

        public IStat GetStat(StatType type) => StatsManager.GetStat(type);
    }
}
