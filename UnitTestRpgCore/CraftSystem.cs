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

namespace UnitTestRpgCore
{
    [TestClass]
    public class CraftSystem
    {
        Player hero;
#warning add recept to inventory... after craft destroy it?

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

            Receipt recept = new Receipt(new Equipment(111, "sword of destiny", "ultimate weapon", EquipSlot.RightHand), materials, 333, "Sword of Destiny Receipt", "Receipt for ultimate weapon");

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

            Receipt recept = new Receipt(new Equipment(111, "sword of destiny", "ultimate weapon", EquipSlot.RightHand), materials, 333, "Sword of Destiny Receipt", "Receipt for ultimate weapon");

            hero.Interact(new Resources(11, "iron", "resource item"));
            hero.Interact(new Resources(11, "iron", "resource item"));
            hero.Interact(new Resources(11, "iron", "resource item"));

            hero.Interact(new Resources(12, "wood", "resource item"));
            hero.Interact(new Resources(12, "wood", "resource item"));

            hero.Interact(new Resources(13, "coal", "resource item"));
            hero.Interact(new Resources(13, "coal", "resource item"));
            hero.Interact(new Resources(13, "coal", "resource item"));
            hero.Interact(new Resources(13, "coal", "resource item"));

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
            Equipment receipt_weapon = new Equipment(111, "sword of destiny", "ultimate weapon", EquipSlot.RightHand);

            receipt_weapon.AddEquipEffect(new EquipEffect(EffectTarget.Character, StatType.Luck, +20f));
            Receipt recept = new Receipt(receipt_weapon, materials, 333, "Sword of Destiny Receipt", "Receipt for ultimate weapon");

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

            // pick recept
            hero.Interact(recept);
            hero.Craft(recept);
            Assert.AreEqual(5, hero.Inventory.Items.Count);

            Assert.AreEqual(2, hero.Inventory.Items.Where(item => item.Id == 12).Count());
            Assert.AreEqual(1, hero.Inventory.Items.Where(item => item.Id == 11).Count());
            Assert.AreEqual(0, hero.Inventory.Items.Where(item => item.Id == 13).Count());

            IItem newItem = hero.Inventory.Items.Find(x => x.Id == 111);
            Assert.AreEqual(true, hero.Inventory.Exist(recept.Result));
            Assert.AreEqual("sword of destiny", newItem.Name);
            Assert.AreEqual("ultimate weapon", newItem.Description);
            Assert.AreEqual(EquipSlot.RightHand, ((Equipment)newItem).Slot);
        }
    }
}
