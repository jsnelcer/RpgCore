using Microsoft.VisualStudio.TestTools.UnitTesting;
using RpgCore.Enum;
using RpgCore.Items;
using RpgCore.Stats;
using RpgCore.Storaged;
using RpgCore.Inteface;
using RpgCore;
using System.Collections.Generic;
using System.Linq;
using RpgCore.Crafting;

namespace UnitTestRpgCore
{
    [TestClass]
    public class CraftSystem
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
        public void Receipt()
        {

            Resources iron = new Resources(11, "iron", "resource item");
            Resources wood = new Resources(12, "wood", "resource item");
            Resources coal = new Resources(13, "coal", "resource item");

            Dictionary<IItem, int> materials = new Dictionary<IItem, int>
            {
                {iron, 3 },
                {wood, 2 },
                {coal, 4 }
            };

            Receipt recept = new Receipt(new Equipment(111, "sword of destiny", "ultimate weapon", EquipSlot.RightHand), materials);

            Assert.AreEqual(111, recept.Result.Id);
            Assert.AreEqual("sword of destiny", recept.Result.Name);
            Assert.AreEqual("ultimate weapon", recept.Result.Description);
            Assert.AreEqual(3, recept.Materials.Count);
            Assert.AreEqual(3, recept.Materials[iron]);
            Assert.AreEqual(2, recept.Materials[wood]);
            Assert.AreEqual(4, recept.Materials[coal]);
        }

        [TestMethod]
        public void CheckMaterial()
        {

            Resources iron = new Resources(11, "iron", "resource item");
            Resources wood = new Resources(12, "wood", "resource item");
            Resources coal = new Resources(13, "coal", "resource item");

            Dictionary<IItem, int> materials = new Dictionary<IItem, int>
            {
                {iron, 3 },
                {wood, 2 },
                {coal, 4 }
            };

            Receipt recept = new Receipt(new Equipment(111, "sword of destiny", "ultimate weapon", EquipSlot.RightHand), materials);

            hero.PickUp(new Resources(11, "iron", "resource item"));
            hero.PickUp(new Resources(11, "iron", "resource item"));
            hero.PickUp(new Resources(11, "iron", "resource item"));

            hero.PickUp(new Resources(12, "wood", "resource item"));
            hero.PickUp(new Resources(12, "wood", "resource item"));

            hero.PickUp(new Resources(13, "coal", "resource item"));
            hero.PickUp(new Resources(13, "coal", "resource item"));
            hero.PickUp(new Resources(13, "coal", "resource item"));
            hero.PickUp(new Resources(13, "coal", "resource item"));

            Assert.AreEqual(true, recept.CanCraft(hero.Inventory));
        }

        [TestMethod]
        public void LetsCraft()
        {

            Resources iron = new Resources(11, "iron", "resource item");
            Resources wood = new Resources(12, "wood", "resource item");
            Resources coal = new Resources(13, "coal", "resource item");

            Dictionary<IItem, int> materials = new Dictionary<IItem, int>
            {
                {iron, 3 },
                {wood, 2 },
                {coal, 4 }
            };

            Receipt recept = new Receipt(new Equipment(111, "sword of destiny", "ultimate weapon", EquipSlot.RightHand), materials);

            hero.PickUp(new Resources(11, "iron", "resource item"));
            hero.PickUp(new Resources(11, "iron", "resource item"));
            hero.PickUp(new Resources(11, "iron", "resource item"));

            hero.PickUp(new Resources(12, "wood", "resource item"));
            hero.PickUp(new Resources(12, "wood", "resource item"));

            hero.PickUp(new Resources(13, "coal", "resource item"));
            hero.PickUp(new Resources(13, "coal", "resource item"));
            hero.PickUp(new Resources(13, "coal", "resource item"));
            hero.PickUp(new Resources(13, "coal", "resource item"));

            IItem newItem = recept.Craft(hero.Inventory);

            ////////

            hero.PickUp(new Resources(11, "iron", "resource item"));
            hero.PickUp(new Resources(11, "iron", "resource item"));
            hero.PickUp(new Resources(11, "iron", "resource item"));

            hero.PickUp(new Resources(12, "wood", "resource item"));
            hero.PickUp(new Resources(12, "wood", "resource item"));

            hero.PickUp(new Resources(13, "coal", "resource item"));
            hero.PickUp(new Resources(13, "coal", "resource item"));
            hero.PickUp(new Resources(13, "coal", "resource item"));
            hero.PickUp(new Resources(13, "coal", "resource item"));

            IItem newItem_2 = recept.Craft(hero.Inventory);
            ((Equipment)newItem_2).AddEquipEffect(new EquipEffect(EffectTarget.Character, StatType.Health, 60f));
            //////




            Assert.AreEqual(111, newItem.Id);
            Assert.AreEqual("sword of destiny", newItem.Name);
            Assert.AreEqual("ultimate weapon", newItem.Description);
            Assert.AreEqual(EquipSlot.RightHand, ((Equipment)newItem).Slot);
            Assert.AreEqual(0, hero.Inventory.Items.Count);
        }
    }
}
