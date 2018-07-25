using RpgCore;
using RpgCore.Enum;

namespace RpgCore.Items
{
    public class ConsumableItem : Item, IUseable
    {
        public Effect Effect { private set; get; }
        public ConsumableItem(int id, string name, string description, Effect value)
            :base(id, name ,description)
        {
            this.Effect = value;
        }

        internal ConsumableItem(int id, string name, string description)
                : base(id, name, description)
        {
           
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