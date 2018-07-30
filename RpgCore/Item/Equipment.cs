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
        public List<EquipEffect> EquipEffects { get; internal set; }

        public Equipment(int _id, string _name, string _description, EquipSlot _slot)
        {
            this.id = _id;
            this.name = _name;
            this.description = _description;
            this.Slot = _slot;
            EquipEffects = new List<EquipEffect>();
            Equiped = false;
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
