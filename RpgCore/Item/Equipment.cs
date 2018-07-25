using System;
using System.Collections.Generic;
using System.Linq;
using RpgCore.Stats;
using RpgCore.Enum;

namespace RpgCore.Items
{
    public class Equipment : Item
    {
        public EquipSlot Slot { get; private set; }
        private List<EquipEffect> EquipEffects { get; set; }
        //private List<Effect> Effects { get; set; }
        //private List<Stat> RecomendetAtribute { get; set; } 

        public Equipment(int id, string name, string description, EquipSlot slot)
            :base(id, name, description)
        {
            this.Slot = slot;
            EquipEffects = new List<EquipEffect>();
            //Effects = new List<Effect>();
            //RecomendetAtribute = new List<Stat>();
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
            EquipEffects.Remove(effect);
        }

        public List<EquipEffect> GetEquipEffects()
        {
            if (EquipEffects.Any())
            {
                return EquipEffects;
            }
            else
            {
                return null;
            }
        }

        /*
        public void AddEffect(Effect effect)
        {
            if (!Effects.Exists(x => x.TargetStat == effect.TargetStat))
            {
                Effects.Add(effect);
            }
            else
            {
                Effects.Find(x => x.TargetStat == effect.TargetStat).IncreastValue(effect.Value);
            }
        }

        public void RemoveEffect(Effect effect)
        {
            Effects.Remove(effect);
        }

        public List<Effect> GetEffects()
        {
            if (Effects.Any())
            {
                return Effects;
            }
            else
            {
                return null;
            }
        }
        */
    }
}
