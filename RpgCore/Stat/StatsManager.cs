using System;
using System.Collections.Generic;
using System.Linq;
using RpgCore.Stats;
using RpgCore.Enum;
using RpgCore.Inteface;
using RpgCore.Items;

namespace RpgCore
{
    public class StatsManager
    {
        public List<Stat> Stats { get; private set; }
        public List<TimeEffect> DurationEffect { get; private set; }

        public StatsManager(List<Stat> baseStats)
        {
            Stats = new List<Stat>();
            DurationEffect = new List<TimeEffect>();
            AddStats(baseStats);
        }

        internal void AddStats(List<Stat> baseStats)
        {
            this.Stats = baseStats;
        }

        public Stat GetStat(StatType type)
        {
            return Stats.Find(x => x.Type == type);
        }
        
        public void ApplyEffect(IEffect<StatsManager> effect)
        {
            effect.ApplyEffect(this);
        }

        public void UpdateStats()
        {
            if(DurationEffect.Any())
            {

                DurationEffect.ForEach(effect =>
                {
                    if (effect.Stack > 0)
                    {
                        Stats.Find(x => x.Type == effect.TargetStat).ChangeBaseValue(effect);
                    }
                    effect.Used();
                });

                DurationEffect.ForEach(effect =>
                {
                    if (effect.Stack < 0)
                    {
                        Stats.Find(x => x.Type == effect.TargetStat).RemoveAllModifier(effect);
                    }
                });
            }
        }

        public void EquipStats(List<Equipment> equip)
        {
            Stats.ForEach(stat =>
            {
                stat.RemoveEquipModifier();
                equip.ForEach(x =>
                {
                    var mod = x.EquipEffects.Where(y => y.TargetStat == stat.Type).FirstOrDefault();
                    if (mod != null)
                    {
                        stat.AddModifier(mod);
                    }
                });
            });
            
        }
    }
}