using System;
using System.Collections.Generic;
using System.Linq;
using RpgCore.Storaged;
using RpgCore.Stats;
using RpgCore.Items;
using RpgCore.Enum;

namespace RpgCore
{
    public class Player : Character
    {
        private StorageManager storageManager = StorageManager.Instance;
        private StatsManager statsManager;

        public Player(string name, string description, List<Stat> baseStats)
            :base(name, description)
        {
            statsManager = new StatsManager(baseStats);
            storageManager.EquipChange += statsManager.EquipStats;
        }

        public void UseItem(IUseable item) => AddEffect(item.Use());

        public void PickUp(Item item)
        {
            storageManager.Add2Inventory(item);
        }

        public void Equip(Equipment item)
        {
            storageManager.FromInventory2Equip(item);
        }

        public void Interact(IInteractable item) => item.Interact();

        public void AddEffect(Effect effect)
        {
            if(effect.Target == EffectTarget.Character)
            {
                statsManager.ApplyEffect(effect);    
            }
            else
            {
                storageManager.ApplyEffect(effect);
            }
        } 
        public void Update()
        {
            statsManager.UpdateStats();
        }

        public Stat GetStat(StatType type) => statsManager.GetStat(type);
    }
}
