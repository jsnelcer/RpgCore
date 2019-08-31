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
        Player hero;

        IEnemy enemy_0;
        IEnemy enemy_1;
        IEnemy enemy_2;
        IEnemy enemy_3;
        IEnemy enemy_4;

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

            enemy_0 = new Enemy("Orc", "ver. 3.3", orc_stats, new Storage<IItem>(), new Storage<ConsumableItem>(), new Storage<IEquiped>());
            enemy_1 = new Enemy("Orc", "ver. 3.3", orc_stats, new Storage<IItem>(), new Storage<ConsumableItem>(), new Storage<IEquiped>());
            enemy_2 = new Enemy("Orc", "ver. 3.3", orc_stats, new Storage<IItem>(), new Storage<ConsumableItem>(), new Storage<IEquiped>());
            enemy_3 = new Enemy("Orc", "ver. 3.3", orc_stats, new Storage<IItem>(), new Storage<ConsumableItem>(), new Storage<IEquiped>());
            enemy_4 = new Enemy("Orc", "ver. 3.3", orc_stats, new Storage<IItem>(), new Storage<ConsumableItem>(), new Storage<IEquiped>());

            hero = new Player("Kazisvet III.", "z Bozi vule král", stats, new Storage<IItem>(), new Storage<ConsumableItem>(), new Storage<IEquiped>());

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
        public void CreateKillQuestTest()
        {
            IQuest quest = new KillQuest(1, "Welcome", "First Quest", itemReward, statReward, enemy_0, 5);

            Assert.AreEqual(1, quest.Id);
            
            Assert.AreEqual("Welcome", quest.Title);
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
            IQuest quest = new KillQuest(1, "Welcome", "First Quest", itemReward, statReward, enemy_0, 5);
            quest.AcceptQuest(hero);

            Assert.AreEqual(1, hero.QuestList.Count);
            Assert.AreEqual(true, quest.Active);
            Assert.AreEqual(false, quest.IsComplete());
            Assert.AreEqual($"Kill Orc: 0/5", quest.GetConditions());
        }

        [TestMethod]
        public void UpdateKillQuestTest()
        {
            IQuest quest = new KillQuest(1, "Welcome", "First Quest", itemReward, statReward, enemy_0, 5);
            quest.AcceptQuest(hero);

            Assert.AreEqual(true, quest.Active);
            Assert.AreEqual(false, quest.IsComplete());
            
            hero.Attack(enemy_1);
            Assert.AreEqual($"Kill Orc: 1/5", quest.GetConditions());

            Assert.AreEqual(false, hero.CompleteQuest(quest));
        }

        [TestMethod]
        public void CompleteKillQuestTest()
        {
            IQuest quest = new KillQuest(1, "Welcome", "First Quest", itemReward, statReward, enemy_0, 5);
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
    }
}
