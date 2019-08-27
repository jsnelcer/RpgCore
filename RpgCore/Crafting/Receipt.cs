using RpgCore.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
namespace RpgCore.Crafting
{
    public class Receipt : IItem
    {
        public IItem Result { get; private set; }
        public Dictionary<IItem, int> Materials { get; private set; }    //metrial, count

        private int id { get; set; }
        private string description { get; set; }
        private string name { get; set; }

        public int Id => id;
        public string Name => name;
        public string Description => description;

        public Receipt(IItem result, Dictionary<IItem, int> materials, int id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;

            Result = result.Clone() ?? throw new ArgumentNullException(nameof(result));
            Materials = materials ?? throw new ArgumentNullException(nameof(materials));
        }

        public Receipt(Receipt anotherReceipt)
        {
            this.id = anotherReceipt.Id;
            this.name = anotherReceipt.Name;
            this.description = anotherReceipt.Description;

            Result = anotherReceipt.Result.Clone();
            Materials = new Dictionary<IItem, int>(anotherReceipt.Materials);
        }


        public bool CanCraft(IStorage<IItem> storage)
        {
            bool craft = true;
            Materials.ToList().ForEach(material =>
            {
                if (storage.Items.Where(x => x.Id == material.Key.Id).Count() < material.Value)
                {
                    craft = false;
                    return;
                }
            });

            return craft;
        }

        public IItem Craft(IStorage<IItem> storage)
        {
            if (CanCraft(storage))
            {
                Materials.ToList().ForEach(material =>
                {
                    if (storage.Items.Where(x => x.Id == material.Key.Id).Count() >= material.Value)
                    {
                        var usedItems = storage.Items.Where(x => x.Id == material.Key.Id).Take(material.Value).ToList();
                        usedItems.ForEach(item =>
                        {
                            storage.RemoveItem(item);
                        });
                        
                    }
                });
                IItem clon = Result.Clone();
                return clon;
            }
            return null;
        }
        
        public IItem Clone() => new Receipt(this);
    }
}
