using RpgCore;
using RpgCore.Enum;
using RpgCore.Inteface;

namespace RpgCore.Items
{
    public class ConsumableItem : IItem, IUseable
    {
        private int id { get; set; }
        private string description { get; set; }
        private string name { get; set; }

        public int Id => id;
        public string Name => name;
        public string Description => description;

        public Effect Effect { private set; get; }

        public ConsumableItem(int id, string name, string description, Effect value)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.Effect = value;
        }
        
        public void SetEffect(Effect value)
        {
            if(Effect == null)
            {
                this.Effect = value;
            }
        }

        public Effect Use()
        {
            return Effect;
        }
    }
}