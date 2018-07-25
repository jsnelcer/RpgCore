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
    }
}
