using System;
using System.Collections.Generic;
using System.Linq;
using RpgCore.Enum;
using RpgCore.Inteface;

namespace RpgCore.Items
{
    public class Equipment : IEquiped
    {
        private int id { get; set; }
        private string description { get; set; }
        private string name { get; set; }
        

        public int Id => id;
        public string Name => name;
        public string Description => description;

        public bool Equiped { get; set; }
        public EquipSlot Slot { get; private set; }
        public List<IEffect> EquipEffects { get; internal set; }

        public Equipment(int _id, string _name, string _description, EquipSlot _slot)
        {
            this.id = _id;
            this.name = _name;
            this.description = _description;
            this.Slot = _slot;
            EquipEffects = new List<IEffect>();
            Equiped = false;
        }


        protected Equipment(Equipment anotherEquip)
        {
            this.id = anotherEquip.Id;
            this.name = anotherEquip.Name;
            this.description = anotherEquip.Description;
            this.Slot = anotherEquip.Slot;
            this.EquipEffects = new List<IEffect>(anotherEquip.EquipEffects);
            this.Equiped = anotherEquip.Equiped;
        }

        public void AddEquipEffect(IEffect effect)
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


        public IItem Clone()
        {
            return new Equipment(this);
        }
    }
}
