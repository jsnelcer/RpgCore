using System;
using System.Collections.Generic;
using System.Linq;
using RpgCore.Enum;
using RpgCore.Inteface;

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
                        Stats.Find(x => x.Type == effect.TargetStat).DurationEffectStep(effect);
                    }
                    effect.Used();
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

        internal void EquipStats(List<IEquiped> items)
        {
            throw new NotImplementedException();
        }

        //public void EquipStats(List<IEquiped> equip)
        //{
        //    Stats.ForEach(stat =>
        //    {
        //        stat.RemoveEquipModifier();
        //        equip.ForEach(x =>
        //        {
        //            var mod = ((Equipment)x).EquipEffects.Where(y => y.TargetStat == stat.Type).FirstOrDefault();
        //            if (mod != null)
        //            {
        //                stat.AddModifier(mod);
        //            }
        //        });
        //    });

        //}
    }
}