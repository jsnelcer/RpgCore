using System;
using System.Collections.Generic;
using System.Linq;
using RpgCore.Crafting;
using RpgCore.StateMachine;
using RpgCore.Items;
using RpgCore.Enum;
using RpgCore.Interface;
using RpgCore.Stats;
using RpgCore.Quest;

namespace RpgCore
{
    public class Player : IFighter
    {
        private int id { get; set; }
        private string description { get; set; }
        private string name { get; set; }

        public StateMachineSystem StateMachine { get; private set; }

        public IState CurrentState => StateMachine.CurrentState;

        public int Id => id;
        public string Name => name;
        public string Description => description;

        public IStorage<IItem> Inventory { get; private set; }
        public IStorage<ConsumableItem> QuickUse { get; private set; }
        public IStorage<IEquiped> Equip { get; private set; }
        
        private StatsManager StatsManager;
        public List<IQuest> QuestList;
        
        public delegate void EquipChangeEvent();
        public static event EquipChangeEvent EquipChange;

        public delegate void KillEnemyEvent(IFighter target);
        public static event KillEnemyEvent KillEnemy;

        public delegate void PickItemEvent(IItem item);
        public static event PickItemEvent PickItem;

        public Player(string name, string description, List<IStat> baseStats, IStorage<IItem> inventory, IStorage<ConsumableItem> quickUse, IStorage<IEquiped> equip)
        {
            this.id = 0;
            this.name = name;
            this.description = description;

            StatsManager = new StatsManager(baseStats);
            QuestList = new List<IQuest>();

            StateMachine = new StateMachineSystem();
            StateMachine.ChangeState(new Idle(this));

            this.Inventory = inventory;
            this.QuickUse = quickUse;
            this.Equip = equip;

            EquipChange += UpdateStatsFromEquip;
        }
        
        public void UseItem(IUseable<IEffect> item)
        {
            IEffect eff = item.Use();
            StatsManager.ApplyEffect(eff);
        }
        
        public void Interact(IInteractable entity) => entity.Interact(this);
                
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
        
        public void AddEffect(IEffect effect) =>  StatsManager.ApplyEffect(effect);
        
        public void Update() => StatsManager.UpdateStats();

        public IStat GetStat(StatType type) => StatsManager.GetStat(type);

        public List<IItem> InventoryFilter(string contain)
        {
            return Inventory.Items.Where(x => x.Name.Contains(contain) || x.Description.Contains(contain)).ToList();
        }

        public void Craft(Receipt receipt)
        {
            if(receipt.CanCraft(this.Inventory))
            {
                Inventory.AddItem(receipt.Craft(this.Inventory));
            }
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

                if(!target.Alive())
                {
                    KillEnemy?.Invoke(target);
                }
            }
            else
            {
                StateMachine.Switch2PreviousState();
            }
        }
        
        public void Hit(List<IEffect> attack)
        {
            if (CurrentState.Type != StateType.Death)
            {
                attack.ForEach(e => this.AddEffect(e));

#if Debug
                Console.WriteLine(this.Name + ": " + GetStat(StatType.Health).Value + "/" + ((RegenerationStat)GetStat(StatType.Health)).MaxValue);
#endif

                if (GetStat(StatType.Health).Value <= 0)
                {
                    StateMachine.ChangeState(new Death(this));
                }
            }
        }

        public List<IEntity> LookAround()
        {
            List<IEntity> result = new List<IEntity>();
            return result;
        }

        private void UpdateStatsFromEquip() => StatsManager.EquipStats(Equip.Items);

        private IEquiped GetItemFromSlot(EquipSlot slot)
        {
            return Equip.Items.Where(x=>x.Slot == slot).FirstOrDefault();
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
            PickItem?.Invoke(item);
            Inventory.AddItem(item);
        }

        public void UpgradeStat(IStat stat)
        {
            StatsManager.UpgradeStat(stat);
        }

        public void AddQuest(IQuest quest)
        {
            QuestList.Add(quest);

            switch (quest.Type)
            {
                case QuestType.Kill:
                    KillEnemy += quest.UpdateQuest;
                    break;
                case QuestType.Delivery:
                    break;
                case QuestType.Gather:
                    PickItem += quest.UpdateQuest;
                    break;
                case QuestType.Escort:
                    break;
                case QuestType.Craft:
                    break;
                default:
                    break;
            }
        }

        public bool CompleteQuest(IQuest quest)
        {
            bool result = quest.CompleteQuest(this);

            if (result)
            {
                switch (quest.Type)
                {
                    case QuestType.Kill:
                        KillEnemy -= quest.UpdateQuest;
                        break;
                    case QuestType.Delivery:
                        break;
                    case QuestType.Gather:
                        PickItem -= quest.UpdateQuest;
                        break;
                    case QuestType.Escort:
                        break;
                    case QuestType.Craft:
                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        public List<IItem> GetInventory()
        {
            return Inventory.Items;
        }
    }
}
