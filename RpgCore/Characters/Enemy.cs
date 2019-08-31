using System;
using System.Collections.Generic;
using System.Linq;
using RpgCore.Interface;
using RpgCore.Enum;
using RpgCore.StateMachine;
using RpgCore.Items;
using RpgCore.Stats;

namespace RpgCore
{
    public class Enemy : IEnemy
    {
        public string Name => this.name;
        public string Description => this.description;
        public int Id => id;

        public StateMachineSystem StateMachine { get; private set; }

        public IState CurrentState => StateMachine.CurrentState;
        private int id { get; set; }
        private string description { get; set; }
        private string name { get; set; }


        public IStorage<IItem> Inventory { get; private set; }
        public IStorage<ConsumableItem> QuickUse { get; private set; }
        public IStorage<IEquiped> Equip { get; private set; }

        private StatsManager StatsManager;

        public delegate void EquipChangeEvent();
        public static event EquipChangeEvent EquipChange;

        public Enemy(string name, string description, List<IStat> baseStats, IStorage<IItem> inventory, IStorage<ConsumableItem> quickUse, IStorage<IEquiped> equip)
        {
            this.id = 0;
            this.name = name;
            this.description = description;

            StatsManager = new StatsManager(baseStats);

            this.Inventory = inventory;
            this.QuickUse = quickUse;
            this.Equip = equip;

            EquipChange += UpdateStatsFromEquip;


            StateMachine = new StateMachineSystem();
            StateMachine.ChangeState(new Idle(this));
        }

        public void AddEffect(IEffect effect) => StatsManager.ApplyEffect(effect);

        private void UpdateStatsFromEquip() => StatsManager.EquipStats(Equip.Items);

        public override string ToString()
        {
            return this.Name + ": " + this.Description;
        }

        public List<IEntity> LookAround()
        {
            List<IEntity> result = new List<IEntity>();
            return result;
        }

        private IEquiped GetItemFromSlot(EquipSlot slot)
        {
            return Equip.Items.Where(x => x.Slot == slot).FirstOrDefault();
        }

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
        public void Attack(IFighter target)
        {
            if (target.Alive())
            {
                List<IEffect> dmg = new List<IEffect>();
                IEquiped weapon = this.GetItemFromSlot(EquipSlot.RightHand);
                if (weapon != null)
                {
                    weapon.EquipEffects.ForEach(x =>
                    {
                        dmg.Add(x);
                    });
                }
                else
                {
                    dmg.Add(new InstantEffect(EffectTarget.Character, StatType.Health, -20));
                }

                target.Hit(dmg);
            }
            else
            {
                StateMachine.Switch2PreviousState();
            }
        }

        public IStat GetStat(StatType type) => StatsManager.GetStat(type);

        public void Hit(List<IEffect> attack)
        {
            attack.ForEach(e => this.AddEffect(e));
            Console.WriteLine(this.Name + ": " + GetStat(StatType.Health).Value + "/" + ((RegenerationStat)GetStat(StatType.Health)).MaxValue);
            if (GetStat(StatType.Health).Value <= 0 && CurrentState.Type != StateType.Death)
            {
                StateMachine.ChangeState(new Death(this));
            }
        }
        public void Move()
        {
            #warning rly need this?
        }

        public bool Alive()
        {
            return CurrentState.Type != StateType.Death;
        }

        List<IInteractable> ICharacter.LookAround()
        {
            List<IInteractable> result = new List<IInteractable>();
            return result;
        }

        public void AddToInventory(IItem item)
        {
            Inventory.AddItem(item);
        }

        public void UpgradeStat(IStat stat)
        {
            StatsManager.UpgradeStat(stat);
        }

        public void AddQuest(IQuest quest)
        {
            #warning rly need this?
        }

        public bool CompleteQuest(IQuest quest)
        {
            #warning rly need this?
            return false;
        }

        public List<IItem> GetInventory()
        {
            return Inventory.Items;
        }
    }
}
