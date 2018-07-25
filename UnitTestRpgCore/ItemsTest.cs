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
    public class ItemsTest
    {
        ConsumableItem healthPotion;
        Equipment helm;
        Resources iron;

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
            
            healthPotion = (ConsumableItem)Item.CreateItem(ItemType.Consumable, 99, "Health of Potion", "Get 40hp");
            Effect restoreHealth = new Effect(EffectTarget.Character, StatType.Health, 40f);
            healthPotion.SetEffect(restoreHealth);

            helm = (Equipment)Item.CreateItem(ItemType.Equip, 999, "helm of fire", "fireeee");
            EquipEffect increaseHealth = new EquipEffect(EffectTarget.Character, StatType.Health, +30f);
            helm.AddEquipEffect(increaseHealth);
            helm.SetSlot(EquipSlot.Head);

            iron = (Resources)Item.CreateItem(ItemType.Resources, 19, "iron", "resource item");
        }

        [TestMethod]
        public void CreateConsumableItemTest()
        {
            Effect restoreHealth = new Effect(EffectTarget.Character, StatType.Health, 40f);

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
            EquipEffect increaseHealth = new EquipEffect(EffectTarget.Character, StatType.Health, +30f);

            Assert.AreEqual(999, helm.Id);
            Assert.AreEqual(EquipSlot.Head, helm.Slot);
            Assert.AreEqual("helm of fire", helm.Name);
            Assert.AreEqual("fireeee", helm.Description);

            Assert.AreEqual(increaseHealth.Target, helm.EquipEffects[0].Target);
            Assert.AreEqual(increaseHealth.TargetStat, helm.EquipEffects[0].TargetStat);
            Assert.AreEqual(increaseHealth.Value, helm.EquipEffects[0].Value);
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
            hero.PickUp(helm);

            Assert.AreEqual(hero.Inventory.Items[0], helm);
            
        }
    }
}
