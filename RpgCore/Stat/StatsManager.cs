using System;
using System.Collections.Generic;
using System.Linq;
using RpgCore.Enum;
using RpgCore.Interface;

namespace RpgCore
{
    public class StatsManager
    {
        public List<IStat> Stats { get; private set; }
        public List<TimeEffect> DurationEffect { get; private set; }

        public StatsManager(List<IStat> baseStats)
        {
            Stats = new List<IStat>();
            DurationEffect = new List<TimeEffect>();
            AddStats(baseStats);
        }

        internal void AddStats(List<IStat> baseStats)
        {
            this.Stats = baseStats;
        }

        public IStat GetStat(StatType type)
        {
            return Stats.Find(x => x.Type == type);
        }
        
        public void ApplyEffect(IEffect effect)
        {            
            if(effect.GetType() == typeof(TimeEffect))
            {
                DurationEffect.Add((TimeEffect)effect);
                effect.ApplyEffect(Stats.Find(x => x.Type == effect.TargetStat));
            }
            else
            {
                effect.ApplyEffect(Stats.Find(x => x.Type == effect.TargetStat));
            }
        }

        public void UpdateStats()
        {
            if(DurationEffect.Any())
            {
                DurationEffect.ForEach(effect =>
                {
                    if (effect.Stack >= 0)
                    {
                        effect.ApplyEffect(Stats.Find(x => x.Type == effect.TargetStat));
                    }
                });

                DurationEffect.ForEach(effect =>
                {
                    if (effect.Stack < 0)
                    {
                        Stats.Find(x => x.Type == effect.TargetStat).DurationEffectEnd(effect);
                    }
                });
            }
        }

        public void EquipStats(List<IEquiped> equip)
        {
            RemoveEquipEffects();
            equip.ForEach(item =>
                {
                    item.EquipEffects.ForEach(effect =>
                    {
                        effect.ApplyEffect(Stats.Find(x => x.Type == effect.TargetStat));
                    });
                });
        }

        public void UpgradeStat(IStat upgradeStat)
        {
            IStat stat = GetStat(upgradeStat.Type);
            stat.UpgradeStat(upgradeStat.Value);
        }

        private void RemoveEquipEffects()
        {
            Stats.ForEach(x => x.RemoveEquipEffects());
        }
    }
}