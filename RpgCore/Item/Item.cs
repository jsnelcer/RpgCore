using RpgCore.Enum;

namespace RpgCore.Items
{
    public abstract class Item
    {
        public int Id { get; private set; }
        public string Name
        {
            private set; get;
        }
        public string Description
        {
            get; private set;
        }

        public Item(int id, string name, string description)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
        }

        public override string ToString() 
        {
            string result = this.Name + ": " + this.Description;
            
            return result;
        }

        public static Item CreateItem(ItemType type, int id, string name, string description)
        {
            switch (type)
            {
                case ItemType.Equip:
                    return new Equipment(id, name, description);
                case ItemType.Consumable:
                    return new ConsumableItem(id, name, description);
                case ItemType.Resources:
                    return new Resources(id, name, description);
                default:
                    return null;
            }
        }
    }
}
