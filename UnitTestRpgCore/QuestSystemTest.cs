using Microsoft.VisualStudio.TestTools.UnitTesting;
using RpgCore.Enum;
using RpgCore.Items;
using RpgCore.Stats;
using RpgCore.Storaged;
using RpgCore.Interface;
using RpgCore;
using System.Collections.Generic;
using System.Linq;
using RpgCore.Crafting;
using RpgCore.Quest;

namespace UnitTestRpgCore
{
    [TestClass]
    public class QuestSystemTest
    {
        Hero hero;

        IEnemy enemy_0;
        IEnemy enemy_1;
        IEnemy enemy_2;
        IEnemy enemy_3;
        IEnemy enemy_4;

        IItem item_0;
        IItem item_1;
        IItem item_2;
        IItem item_3;
        IItem item_4;

        Receipt recept;

        List<IItem> itemReward;
        List<IStat> statReward;

        [TestInitialize]
        public void Init()
        {

            List<IStat> stats = new List<IStat>
            {
                new RegenerationStat(100f, StatType.Health),
                new RegenerationStat(100f, StatType.Energy),
                new RegenerationStat(100f, StatType.Stamina),
                new Stat(0f, StatType.MagicDmg),
                new Stat(30f, StatType.Intelligence),
                new Stat(50f, StatType.Luck)
            };
            List<IStat> orc_stats = new List<IStat>
            {
                new RegenerationStat(1f, StatType.Health),
                new RegenerationStat(100f, StatType.Energy),
                new RegenerationStat(100f, StatType.Stamina),
                new Stat(0f, StatType.MagicDmg),
                new Stat(1f, StatType.Intelligence),
                new Stat(1f, StatType.Luck)
            };

            #region Enemy
            enemy_0 = new Enemy("Orc", "ver. 3.3", orc_stats, new Storage<IItem>(), new Storage<ConsumableItem>(), new Storage<IEquiped>());
            enemy_1 = new Enemy("Orc", "ver. 3.3", orc_stats, new Storage<IItem>(), new Storage<ConsumableItem>(), new Storage<IEquiped>());
            enemy_2 = new Enemy("Orc", "ver. 3.3", orc_stats, new Storage<IItem>(), new Storage<ConsumableItem>(), new Storage<IEquiped>());
            enemy_3 = new Enemy("Orc", "ver. 3.3", orc_stats, new Storage<IItem>(), new Storage<ConsumableItem>(), new Storage<IEquiped>());
            enemy_4 = new Enemy("Orc", "ver. 3.3", orc_stats, new Storage<IItem>(), new Storage<ConsumableItem>(), new Storage<IEquiped>());
            #endregion

            #region Items
            item_0 = new Resources(11, "iron", "resource item");
            item_1 = new Resources(11, "iron", "resource item");
            item_2 = new Resources(11, "iron", "resource item");
            item_3 = new Resources(11, "iron", "resource item");
            item_4 = new Resources(11, "iron", "resource item");
            #endregion

            hero = new Hero("Kazisvet III.", "z Bozi vule král", stats, new Storage<IItem>(), new Storage<ConsumableItem>(), new Storage<IEquiped>());


            #region Receipt

            Resources iron = new Resources(11, "iron", "resource item");
            Resources wood = new Resources(12, "wood", "resource item");
            Resources coal = new Resources(13, "coal", "resource item");


            Dictionary<IItem, int> materials = new Dictionary<IItem, int>
            {
                {iron, 3 },
                {wood, 2 },
                {coal, 4 }
            };
            Equipment receipt_weapon = new Equipment(111, "sword of destiny", "ultimate weapon", EquipSlot.RightHand);

            receipt_weapon.AddEquipEffect(new EquipEffect(EffectTarget.Character, StatType.Luck, +20f));
            recept = new Receipt(receipt_weapon, materials, 333, "Sword of Destiny Receipt", "Receipt for ultimate weapon");

            #endregion

            Equipment helm = new Equipment(1, "Helm of Fire", "+10 fire dmg", EquipSlot.Head);
            helm.AddEquipEffect(new EquipEffect(EffectTarget.Character, StatType.MagicDmg, +10));

            itemReward = new List<IItem>() { helm };

            statReward = new List<IStat>
            {
                new RegenerationStat(10f, StatType.Health),
                new RegenerationStat(5f, StatType.Energy),
                new RegenerationStat(5f, StatType.Stamina),

                new Stat(2f, StatType.Intelligence),
                new Stat(1f, StatType.Luck)
            };
        }

        [TestMethod]
        public void AcceptManyQuestsTest()
        {
            IQuest quest_gather = new QuestGather(1, "Welcome part 2", "Second Quest", itemReward, statReward, item_0, 5);
            IQuest quest_kill = new QuestKill(1, "Welcome part 1", "First Quest", itemReward, statReward, enemy_0, 5);

            quest_gather.AcceptQuest(hero);
            quest_kill.AcceptQuest(hero);

            Assert.AreEqual(2, hero.QuestList.Count);
            Assert.AreEqual(true, quest_gather.Active);
            Assert.AreEqual(true, quest_kill.Active);
        }

        #region Quest Kill

        [TestMethod]
        public void CreateKillQuestTest()
        {
            IQuest quest = new QuestKill(1, "Welcome part 1", "First Quest", itemReward, statReward, enemy_0, 5);

            Assert.AreEqual(1, quest.Id);
            
            Assert.AreEqual("Welcome part 1", quest.Title);
            Assert.AreEqual("First Quest", quest.Description);
            Assert.AreEqual(1, quest.Items.Count);
            Assert.AreEqual(1, quest.Items[0].Id);
            Assert.AreEqual("Helm of Fire", quest.Items[0].Name);
            Assert.AreEqual(5, quest.Stats.Count);
            Assert.AreEqual(QuestType.Kill, quest.Type);
            Assert.AreEqual(false, quest.Active);
        }

        [TestMethod]
        public void AcceptKillQuestTest()
        {
            IQuest quest = new QuestKill(1, "Welcome", "First Quest", itemReward, statReward, enemy_0, 5);
            quest.AcceptQuest(hero);

            Assert.AreEqual(1, hero.QuestList.Count);
            Assert.AreEqual(true, quest.Active);
            Assert.AreEqual(false, quest.IsComplete());
            Assert.AreEqual($"Kill Orc: 0/5", quest.GetConditions());
        }

        [TestMethod]
        public void UpdateKillQuestTest()
        {
            IQuest quest = new QuestKill(1, "Welcome", "First Quest", itemReward, statReward, enemy_0, 5);
            quest.AcceptQuest(hero);

            Assert.AreEqual(true, quest.Active);
            Assert.AreEqual(false, quest.IsComplete());
            
            hero.Attack(enemy_0);
            Assert.AreEqual($"Kill Orc: 1/5", quest.GetConditions());
            hero.Attack(enemy_1);
            Assert.AreEqual($"Kill Orc: 2/5", quest.GetConditions());
            hero.Attack(enemy_2);
            Assert.AreEqual($"Kill Orc: 3/5", quest.GetConditions());
            hero.Attack(enemy_3);
            Assert.AreEqual($"Kill Orc: 4/5", quest.GetConditions());
            
            Assert.AreEqual(false, hero.CompleteQuest(quest));
        }

        [TestMethod]
        public void CompleteKillQuestTest()
        {
            IQuest quest = new QuestKill(1, "Welcome", "First Quest", itemReward, statReward, enemy_0, 5);
            quest.AcceptQuest(hero);

            Assert.AreEqual(true, quest.Active);
            Assert.AreEqual(false, quest.IsComplete());

            hero.Attack(enemy_0);
            hero.Attack(enemy_1);
            hero.Attack(enemy_2);
            hero.Attack(enemy_3);
            hero.Attack(enemy_4);

            Assert.AreEqual($"Kill Orc: 5/5", quest.GetConditions());
            Assert.AreEqual(true, hero.CompleteQuest(quest));

            Assert.AreEqual(hero.Inventory.Items.Count, 1);
            Assert.AreEqual(110f, ((RegenerationStat)hero.GetStat(StatType.Health)).MaxValue);
            Assert.AreEqual(105f, ((RegenerationStat)hero.GetStat(StatType.Energy)).MaxValue);
            Assert.AreEqual(105f, ((RegenerationStat)hero.GetStat(StatType.Stamina)).MaxValue);
            Assert.AreEqual(32f, hero.GetStat(StatType.Intelligence).Value);
            Assert.AreEqual(51f, hero.GetStat(StatType.Luck).Value);
        }

        #endregion

        #region Quest Gather

        [TestMethod]
        public void CreateGatherQuestTest()
        {
            IQuest quest = new QuestGather(1, "Welcome part 2", "Second Quest", itemReward, statReward, item_0, 5);

            Assert.AreEqual(1, quest.Id);

            Assert.AreEqual("Welcome part 2", quest.Title);
            Assert.AreEqual("Second Quest", quest.Description);
            Assert.AreEqual(1, quest.Items.Count);
            Assert.AreEqual(1, quest.Items[0].Id);
            Assert.AreEqual("Helm of Fire", quest.Items[0].Name);
            Assert.AreEqual(5, quest.Stats.Count);
            Assert.AreEqual(QuestType.Gather, quest.Type);
            Assert.AreEqual(false, quest.Active);
        }

        [TestMethod]
        public void AcceptGatherQuestTest()
        {
            IQuest quest = new QuestGather(1, "Welcome part 2", "Second Quest", itemReward, statReward, item_0, 5);
            quest.AcceptQuest(hero);

            Assert.AreEqual(1, hero.QuestList.Count);
            Assert.AreEqual(true, quest.Active);
            Assert.AreEqual(false, quest.IsComplete());
            Assert.AreEqual($"Find iron: 0/5", quest.GetConditions());
        }

        [TestMethod]
        public void UpdateGatherQuestTest()
        {
            IQuest quest = new QuestGather(1, "Welcome part 2", "Second Quest", itemReward, statReward, item_0, 5);
            quest.AcceptQuest(hero);

            Assert.AreEqual(true, quest.Active);
            Assert.AreEqual(false, quest.IsComplete());

            hero.Interact(item_0);
            Assert.AreEqual($"Find iron: 1/5", quest.GetConditions());
            hero.Interact(item_1);
            Assert.AreEqual($"Find iron: 2/5", quest.GetConditions());
            hero.Interact(item_2);
            Assert.AreEqual($"Find iron: 3/5", quest.GetConditions());
            hero.Interact(item_3);
            Assert.AreEqual($"Find iron: 4/5", quest.GetConditions());

            Assert.AreEqual(false, hero.CompleteQuest(quest));
        }

        [TestMethod]
        public void CompleteGatherQuestTest()
        {
            IQuest quest = new QuestGather(1, "Welcome part 2", "Second Quest", itemReward, statReward, item_0, 5);
            quest.AcceptQuest(hero);

            Assert.AreEqual(true, quest.Active);
            Assert.AreEqual(false, quest.IsComplete());

            hero.Interact(item_0);
            hero.Interact(item_1);
            hero.Interact(item_2);
            hero.Interact(item_3);
            hero.Interact(item_4);

            Assert.AreEqual($"Find iron: 5/5", quest.GetConditions());
            Assert.AreEqual(hero.Inventory.Items.Count, 5);

            Assert.AreEqual(true, hero.CompleteQuest(quest));

            Assert.AreEqual(hero.Inventory.Items.Count, 6);
            Assert.AreEqual(110f, ((RegenerationStat)hero.GetStat(StatType.Health)).MaxValue);
            Assert.AreEqual(105f, ((RegenerationStat)hero.GetStat(StatType.Energy)).MaxValue);
            Assert.AreEqual(105f, ((RegenerationStat)hero.GetStat(StatType.Stamina)).MaxValue);
            Assert.AreEqual(32f, hero.GetStat(StatType.Intelligence).Value);
            Assert.AreEqual(51f, hero.GetStat(StatType.Luck).Value);
        }

        #endregion

        #region Quest Craft

        [TestMethod]
        public void CreateCraftQuestTest()
        {
            IQuest quest = new QuestCraft(1, "Welcome part 3", "Craft Quest", itemReward, statReward, recept.Result, 1);

            Assert.AreEqual(1, quest.Id);

            Assert.AreEqual("Welcome part 3", quest.Title);
            Assert.AreEqual("Craft Quest", quest.Description);
            Assert.AreEqual(1, quest.Items.Count);
            Assert.AreEqual(1, quest.Items[0].Id);
            Assert.AreEqual("Helm of Fire", quest.Items[0].Name);
            Assert.AreEqual(5, quest.Stats.Count);
            Assert.AreEqual(QuestType.Craft, quest.Type);
            Assert.AreEqual(false, quest.Active);
        }

        [TestMethod]
        public void AcceptCraftQuestTest()
        {
            IQuest quest = new QuestCraft(1, "Welcome part 3", "Craft Quest", itemReward, statReward, recept.Result, 1);

            quest.AcceptQuest(hero);

            Assert.AreEqual(1, hero.QuestList.Count);
            Assert.AreEqual(true, quest.Active);
            Assert.AreEqual(false, quest.IsComplete());
            Assert.AreEqual($"Craft {recept.Result.Name}: 0/1", quest.GetConditions());
        }

        [TestMethod]
        public void UpdateAndCompleteCraftQuestTest()
        {
            IQuest quest = new QuestCraft(1, "Welcome part 3", "Craft Quest", itemReward, statReward, recept.Result, 1);
            quest.AcceptQuest(hero);

            Assert.AreEqual(true, quest.Active);
            Assert.AreEqual(false, quest.IsComplete());

            hero.Interact(recept);

            #region Gathering
            hero.Interact(new Resources(11, "iron", "resource item"));
            hero.Interact(new Resources(11, "iron", "resource item"));
            hero.Interact(new Resources(11, "iron", "resource item"));
            hero.Interact(new Resources(11, "iron", "resource item"));

            hero.Interact(new Resources(12, "wood", "resource item"));
            hero.Interact(new Resources(12, "wood", "resource item"));
            hero.Interact(new Resources(12, "wood", "resource item"));
            hero.Interact(new Resources(12, "wood", "resource item"));

            hero.Interact(new Resources(13, "coal", "resource item"));
            hero.Interact(new Resources(13, "coal", "resource item"));
            hero.Interact(new Resources(13, "coal", "resource item"));
            hero.Interact(new Resources(13, "coal", "resource item"));
            #endregion

            hero.Craft(recept);

            Assert.AreEqual($"Craft {recept.Result.Name}: 1/1", quest.GetConditions());

            Assert.AreEqual(true, hero.CompleteQuest(quest));
        }

        #endregion
    }
}
