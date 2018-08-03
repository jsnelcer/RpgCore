using RpgCore.Inteface;
using System;
using System.Collections.Generic;
using System.Linq;
namespace RpgCore.Crafting
{
    public class Receipt
    {
        public IItem Result { get; private set; }
        public Dictionary<IItem, int> Materials { get; private set; }    //metrial, count

        public Receipt(IItem result, Dictionary<IItem, int> materials)
        {
            Result = result.Copy() ?? throw new ArgumentNullException(nameof(result));
            Materials = materials ?? throw new ArgumentNullException(nameof(materials));
        }

        public bool CanCraft(IStorage<IItem> storage)
        {
            bool craft = true;
            Materials.ToList().ForEach(material =>
            {
                if (storage.Items.Where(x => x.Id == material.Key.Id).Count() != material.Value)
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
                    if (storage.Items.Where(x => x.Id == material.Key.Id).Count() == material.Value)
                    {
                        storage.Items.RemoveAll(x => x.Id == material.Key.Id);
                    }
                });

                return Result.Copy();
            }
            return null;
        }
    }
}
