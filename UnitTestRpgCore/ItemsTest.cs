using Microsoft.VisualStudio.TestTools.UnitTesting;
using RpgCore.Enum;
using RpgCore.Items;
using RpgCore.Stats;
using RpgCore.Storaged;
using RpgCore.Interface;
using RpgCore;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestRpgCore
{
    [TestClass]
    public class ItemsTest
    {
        ConsumableItem healthPotion;
        Equipment helm;
        Resources iron;

        Hero hero;

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

            hero = new Hero("Kazisvet III.", "z Bozi vule král", stats, new Storage<IItem>(), new Storage<ConsumableItem>(), new Storage<IEquiped>());

            IEffect restoreHealth = new InstantEffect(EffectTarget.Character, StatType.Health, 40f);
            healthPotion = new ConsumableItem(99, "Health of Potion", "Get 40hp", restoreHealth);
            
            helm = new Equipment(999, "helm of fire", "fireeee", EquipSlot.Head);
            EquipEffect increaseHealth = new EquipEffect(EffectTarget.Character, StatType.Health, +30f);
            helm.AddEquipEffect(increaseHealth);

            iron = new Resources(19, "iron", "resource item");
        }

        [TestMethod]
        public void CreateConsumableItemTest()
        {
            IEffect restoreHealth = new InstantEffect(EffectTarget.Character, StatType.Health, 40f);
            healthPotion = new ConsumableItem(99, "Health of Potion", "Get 40hp", restoreHealth);

            Assert.AreEqual(99, healthPotion.Id);
            Assert.AreEqual("Health of Potion", healthPotion.Name);
            Assert.AreEqual("Get 40hp", healthPotion.Description);

            Assert.AreEqual(restoreHealth.Target, healthPotion.Effect.Target);
            Assert.AreEqual(restoreHealth.TargetStat, healthPotion.Effect.TargetStat);
            Assert.AreEqual(restoreHealth.Value, healthPotion.Effect.Value);
        }

        [TestMethod]
        public void CreateEquipItemTest()
        {
            Equipment helm_air = new Equipment(997, "helm of air", "air", EquipSlot.Head);
            EquipEffect increaseHealth = new EquipEffect(EffectTarget.Character, StatType.Health, +30f);
            helm_air.AddEquipEffect(increaseHealth);

            Assert.AreEqual(997, helm_air.Id);
            Assert.AreEqual(EquipSlot.Head, helm_air.Slot);
            Assert.AreEqual("helm of air", helm_air.Name);
            Assert.AreEqual("air", helm_air.Description);

            Assert.AreEqual(increaseHealth.Target, helm_air.EquipEffects[0].Target);
            Assert.AreEqual(increaseHealth.TargetStat, helm_air.EquipEffects[0].TargetStat);
            Assert.AreEqual(increaseHealth.Value, helm_air.EquipEffects[0].Value);
        }

        [TestMethod]
        public void CreateResourcesItemTest()
        {
            Assert.AreEqual(19, iron.Id);
            Assert.AreEqual("iron", iron.Name);
            Assert.AreEqual("resource item", iron.Description);
        }

        [TestMethod]
        public void AddItemsToInventory()
        {
            hero.Interact(helm);
            hero.Interact(healthPotion);
            hero.Interact(iron);

            Assert.AreEqual(hero.Inventory.Items.Count, 3);

            Assert.AreEqual(hero.Inventory.Items[0].Name, helm.Name);
            Assert.AreEqual(hero.Inventory.Items[1].Name, healthPotion.Name);
            Assert.AreEqual(hero.Inventory.Items[2].Name, iron.Name);
        }

        [TestMethod]
        public void ChangeEquip()
        {
            hero.Interact(helm);
            hero.EquipItem(helm);

            Assert.AreEqual(hero.Inventory.Items.Count, 0);
            Assert.AreEqual(hero.Equip.Items.Where(x => x.Slot == EquipSlot.Head).FirstOrDefault(), helm);

            Equipment helm_air = new Equipment(997, "helm of air", "air", EquipSlot.Head);
            hero.Interact(helm_air);

            Assert.AreEqual(helm.Equiped, true);
            Assert.AreEqual(helm_air.Equiped, false);

            hero.EquipItem(helm_air);


            Assert.AreEqual(hero.Inventory.Items.Count, 1);
            Assert.AreEqual(hero.Equip.Items.Where(x => x.Slot == EquipSlot.Head).FirstOrDefault(), helm_air);
            Assert.AreEqual(helm_air.Equiped, true);
            Assert.AreEqual(helm.Equiped, false);
        }


        [TestMethod]
        public void UseConsumableItem()
        {
            IEffect insta = new InstantEffect(EffectTarget.Character, StatType.Health, -60f);
            Assert.AreEqual(100f, hero.GetStat(StatType.Health).Value);
            hero.AddEffect(insta);

            Assert.AreEqual(40f, hero.GetStat(StatType.Health).Value);
            
            hero.UseItem(healthPotion); //+40hp

            Assert.AreEqual(80f, hero.GetStat(StatType.Health).Value);
        }

        [TestMethod]
        public void UseConsumableItem_2()
        {
            IEffect insta = new InstantEffect(EffectTarget.Character, StatType.Health, -40f);
            Assert.AreEqual(100f, hero.GetStat(StatType.Health).Value);
            hero.AddEffect(insta);

            IEffect restoreHealth = new TimeEffect(EffectTarget.Character, StatType.Health, +10f, 3, 1);
            ConsumableItem potion = new ConsumableItem(99, "Health of Potion", "Get 40hp", restoreHealth);
            Assert.AreEqual(60f, hero.GetStat(StatType.Health).Value);

            hero.UseItem(potion); //+10hp/stack x3

            Assert.AreEqual(70f, hero.GetStat(StatType.Health).Value);
            hero.Update();
            Assert.AreEqual(80f, hero.GetStat(StatType.Health).Value);
            hero.Update();
            Assert.AreEqual(90f, hero.GetStat(StatType.Health).Value);
            hero.Update();
            Assert.AreEqual(90f, hero.GetStat(StatType.Health).Value);

            ((TimeEffect)potion.Effect).Refill();
            hero.UseItem(potion);
            Assert.AreEqual(100f, hero.GetStat(StatType.Health).Value);
            hero.Update();
            Assert.AreEqual(100f, hero.GetStat(StatType.Health).Value);
            hero.Update();
            Assert.AreEqual(100f, hero.GetStat(StatType.Health).Value);
        }

    }
}
