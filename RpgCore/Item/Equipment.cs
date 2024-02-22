using System;
using System.Collections.Generic;
using System.Linq;
using RpgCore.Enum;
using RpgCore.Interface;

namespace RpgCore.Items
{
    public class Equipment : IEquiped
    {
        public int Id { get; private set; }
        public string Description { get; private set; }
        public string Name { get; private set; }

        public bool Equiped { get; set; }
        public EquipSlot Slot { get; private set; }
        public List<IEffect> EquipEffects { get; internal set; }

        public Equipment(int _id, string _name, string _description, EquipSlot _slot)
        {
            Id = _id;
            Name = _name;
            Description = _description;
            Slot = _slot;
            EquipEffects = new List<IEffect>();
            Equiped = false;
        }


        protected Equipment(Equipment anotherEquip)
        {
            Id = anotherEquip.Id;
            Name = anotherEquip.Name;
            Description = anotherEquip.Description;
            Slot = anotherEquip.Slot;
            EquipEffects = new List<IEffect>(anotherEquip.EquipEffects);
            Equiped = anotherEquip.Equiped;
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

        public void Interact(ICharacter character)
        {
            character.AddToInventory(this);
        }

        public override string ToString()
        {
            return $"{Name}: {Description}";
        }
    }
}
