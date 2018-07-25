using Microsoft.VisualStudio.TestTools.UnitTesting;
using RpgCore.Enum;
using RpgCore.Items;
using RpgCore.Stats;
using RpgCore.Storaged;
using RpgCore.Inteface;
using RpgCore;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestRpgCore
{
    [TestClass]
    public class EffectTest
    {
        Player hero;

        [TestInitialize]
        public void Init()
        {
            List<Stat> stats = new List<Stat>
            {
                new RegenerationStat(100f, StatType.Health),
                new RegenerationStat(100f, StatType.Energy),
                new RegenerationStat(100f, StatType.Stamina),
                new Stat(30f, StatType.Intelligence),
                new Stat(50f, StatType.Luck)
            };

            hero = new Player("Kazisvet III.", "z Bozi vule král", stats);

        }

        [TestMethod]
        public void TimeEffectApplyOnRegenerationStat()
        {
            IEffect<StatsManager> time = new TimeEffect(EffectTarget.Character, StatType.Health, -10, 3, 1);
            Assert.AreEqual(100f, hero.GetStat(StatType.Health).GetValue());
            hero.AddEffect(time);

            Assert.AreEqual(100f, hero.GetStat(StatType.Health).GetValue());
            //Assert.AreEqual(3, time.Stack);
            hero.Update();

            Assert.AreEqual(90f, hero.GetStat(StatType.Health).GetValue());
            //Assert.AreEqual(2, time.Stack);
            hero.Update();

            Assert.AreEqual(80f, hero.GetStat(StatType.Health).GetValue());
            //Assert.AreEqual(1, time.Stack);
            hero.Update();

            Assert.AreEqual(70f, hero.GetStat(StatType.Health).GetValue());
            //Assert.AreEqual(0, time.Stack);
            hero.Update();

            Assert.AreEqual(70f, hero.GetStat(StatType.Health).GetValue());
            //Assert.AreEqual(-1, time.Stack);
        }

        [TestMethod]
        public void TimeEffectApplyOnStat()
        {
            IEffect<StatsManager> time = new TimeEffect(EffectTarget.Character, StatType.Intelligence, -3, 3, 1);
            Assert.AreEqual(30f, hero.GetStat(StatType.Intelligence).GetValue());
            hero.AddEffect(time);


            Assert.AreEqual(30f, hero.GetStat(StatType.Intelligence).GetValue());
            //Assert.AreEqual(3, time.Stack);
            hero.Update();

            Assert.AreEqual(27f, hero.GetStat(StatType.Intelligence).GetValue());
            //Assert.AreEqual(2, time.Stack);
            hero.Update();

            Assert.AreEqual(24f, hero.GetStat(StatType.Intelligence).GetValue());
            //Assert.AreEqual(1, time.Stack);
            hero.Update();

            Assert.AreEqual(21f, hero.GetStat(StatType.Intelligence).GetValue());
            //Assert.AreEqual(0, time.Stack);
            hero.Update();

            Assert.AreEqual(30f, hero.GetStat(StatType.Intelligence).GetValue());
            //Assert.AreEqual(-1, time.Stack);
        }

        [TestMethod]
        public void InstantEffectApplyOnRegenrationStat()
        {
            IEffect<StatsManager> insta = new InstantEffect(EffectTarget.Character, StatType.Health, -60f);
            Assert.AreEqual(100f, hero.GetStat(StatType.Health).GetValue());
            hero.AddEffect(insta);

            Assert.AreEqual(40f, hero.GetStat(StatType.Health).GetValue());
            RegenerationStat Health = (RegenerationStat)hero.GetStat(StatType.Health);
            Assert.AreEqual(100f, Health.MaxValue);
        }

        [TestMethod]
        public void EquipEffect()
        {
            //IEffect<EquipmentItems> helmEffect = new EquipEffect(EquipSlot.Head, StatType.Health, -60f);
            //Assert.AreEqual(100f, hero.GetStat(StatType.Health).GetValue());
            //hero.AddEffect(helmEffect);

            //Assert.AreEqual(40f, hero.GetStat(StatType.Health).GetValue());
        }


    }
}
