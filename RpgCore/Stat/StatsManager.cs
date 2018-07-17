using System;
using System.Collections.Generic;
using System.Linq;
using RpgCore.Stats;
using RpgCore.Enum;
using RpgCore.Storaged;
using RpgCore.Items;

namespace RpgCore
{
    public class StatsManager
    {
        private List<Stat> Stats = new List<Stat>();
        private List<TimeEffect> DurationEffect = new List<TimeEffect>();

        public StatsManager(List<Stat> baseStats)
        {
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

        public List<Stat> GetStats()
        {
            return Stats;
        }

        internal void ApplyEffect(Effect effect)
        {
            var @switch = new Dictionary<Type, Action> {
                { typeof(Effect), () => {
                                        int index = Stats.FindIndex(x => x.Type == effect.TargetStat);
                                        Stats[index].AddModifier(effect);
                                        } },
                { typeof(EquipEffect), () => {
                                        int index = Stats.FindIndex(x => x.Type == effect.TargetStat);
                                        Stats[index].AddModifier(effect);
                                        } },
                { typeof(TimeEffect), () => ApplyEffect((TimeEffect)effect) },
                { typeof(InstantEffect), () => ApplyEffect((InstantEffect)effect) },
            };

            @switch[effect.GetType()]();
        }

        private void ApplyEffect(InstantEffect effect)
        {
            int index = Stats.FindIndex(x => x.Type == effect.TargetStat);
            if(Stats[index].GetType().Equals((typeof(RegenerationStat))))
            {
                RegenerationStat stat = Stats[index] as RegenerationStat;
                stat.ApplyInstantEffect(effect.Value);
            }            
        }

        private void ApplyEffect(TimeEffect effect)
        {
            if(DurationEffect.Any(x=>x == effect))
            {
                DurationEffect.Find(x=>x == effect).AddStack(effect.Stack);
            }
            else
            {
                DurationEffect.Add(effect);
            }
        }


        public void UpdateStats()
        {
            if(DurationEffect.Any())
            {
                ApplyDurationEffect();
            }
        }

        private void ApplyDurationEffect()
        {
            DurationEffect.ForEach(effect => 
            {
                Stats.Find(x=>x.Type == effect.TargetStat && effect.Stack > 0).ChangeBaseValue(effect.Value);
                effect.Used();
            });

            DurationEffect.RemoveAll(x=>x.Stack <= 0);
        }


        public void EquipStats()
        {
            Console.WriteLine("invoke event");
            
            List<Equipment> equip = StorageManager.Instance.GetEquipItems();

            Stats.ForEach(stat =>
            {
                stat.RemoveEquipModifier();
                equip.ForEach(x =>
                {
                    var mod = x.GetEquipEffects().Where(y => y.TargetStat == stat.Type).FirstOrDefault();
                    if (mod != null)
                    {
                        stat.AddModifier(mod);
                    }
                });
            });
            
        }
    }
}