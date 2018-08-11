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
            List<IStat> stats = new List<IStat>
            {
                new RegenerationStat(100f, StatType.Health),
                new RegenerationStat(100f, StatType.Energy),
                new RegenerationStat(100f, StatType.Stamina),
                new Stat(30f, StatType.Intelligence),
                new Stat(50f, StatType.Luck)
            };

            hero = new Player("Kazisvet III.", "z Bozi vule král", stats, new Storage<IItem>(), new Storage<ConsumableItem>(), new Storage<IEquiped>());

        }

        [TestMethod]
        public void TimeEffectApplyOnRegenerationStat()
        {
            IEffect time = new TimeEffect(EffectTarget.Character, StatType.Health, -10, 3, 1);
            Assert.AreEqual(100f, hero.GetStat(StatType.Health).Value);
            hero.AddEffect(time);

            Assert.AreEqual(90f, hero.GetStat(StatType.Health).Value);
            //Assert.AreEqual(2, time.Stack);
            hero.Update();

            Assert.AreEqual(80f, hero.GetStat(StatType.Health).Value);
            //Assert.AreEqual(1, time.Stack);
            hero.Update();

            Assert.AreEqual(70f, hero.GetStat(StatType.Health).Value);
            //Assert.AreEqual(0, time.Stack);
            hero.Update();

            Assert.AreEqual(70f, hero.GetStat(StatType.Health).Value);
            //Assert.AreEqual(-1, time.Stack);
            hero.Update();

            Assert.AreEqual(70f, hero.GetStat(StatType.Health).Value);
        }

        [TestMethod]
        public void TimeEffectApplyOnStat()
        {
            IEffect time = new TimeEffect(EffectTarget.Character, StatType.Intelligence, -3, 3, 1);
            Assert.AreEqual(30f, hero.GetStat(StatType.Intelligence).Value);
            hero.AddEffect(time);
            
            Assert.AreEqual(27f, hero.GetStat(StatType.Intelligence).Value);
            //Assert.AreEqual(2, time.Stack);
            hero.Update();

            Assert.AreEqual(24f, hero.GetStat(StatType.Intelligence).Value);
            //Assert.AreEqual(1, time.Stack);
            hero.Update();

            Assert.AreEqual(21f, hero.GetStat(StatType.Intelligence).Value);
            //Assert.AreEqual(0, time.Stack);
            hero.Update();
            
            Assert.AreEqual(30f, hero.GetStat(StatType.Intelligence).Value);
            //Assert.AreEqual(-1, time.Stack);
        }


        [TestMethod]
        public void InstantEffectApplyOnRegenerationStat()
        {
            IEffect insta = new InstantEffect(EffectTarget.Character, StatType.Health, -60f);
            Assert.AreEqual(100f, hero.GetStat(StatType.Health).Value);
            hero.AddEffect(insta);

            Assert.AreEqual(40f, hero.GetStat(StatType.Health).Value);
            RegenerationStat Health = (RegenerationStat)hero.GetStat(StatType.Health);
            Assert.AreEqual(100f, Health.MaxValue);
        }

        [TestMethod]
        public void InstantEffectApplyOnStat()
        {
            IEffect insta = new InstantEffect(EffectTarget.Character, StatType.Intelligence, -5f);
            Assert.AreEqual(30f, hero.GetStat(StatType.Intelligence).Value);
            hero.AddEffect(insta);

            Assert.AreEqual(25f, hero.GetStat(StatType.Intelligence).Value);
        }


        [TestMethod]
        public void EquipEffect()
        {
            Assert.AreEqual(100f, hero.GetStat(StatType.Health).Value);

            IEffect helmEffect_health = new EquipEffect(EffectTarget.Character, StatType.Health, 60f);
            IEffect helmEffect_energy = new EquipEffect(EffectTarget.Character, StatType.Energy, -60f);
            IEffect helmEffect_int = new EquipEffect(EffectTarget.Character, StatType.Intelligence, 10f);

            Equipment helm_air = new Equipment(997, "helm of air", "air", EquipSlot.Head);
            helm_air.AddEquipEffect(helmEffect_health);
            helm_air.AddEquipEffect(helmEffect_energy);
            helm_air.AddEquipEffect(helmEffect_int);
            hero.Interact(helm_air);
            hero.EquipItem(helm_air);

            Assert.AreEqual(helm_air.Equiped, true);

            Assert.AreEqual(160f, ((RegenerationStat)hero.GetStat(StatType.Health)).MaxValue);
            Assert.AreEqual(100f, ((RegenerationStat)hero.GetStat(StatType.Health)).Value);

            Assert.AreEqual(40f, ((RegenerationStat)hero.GetStat(StatType.Energy)).MaxValue);
            Assert.AreEqual(40f, ((RegenerationStat)hero.GetStat(StatType.Energy)).Value);

            Assert.AreEqual(40f, hero.GetStat(StatType.Intelligence).Value);



            //change helm

            //hero.EquipItem(
        }
        
    }
}
