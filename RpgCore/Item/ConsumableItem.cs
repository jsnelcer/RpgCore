using RpgCore;

namespace RpgCore.Items
{
    public class ConsumableItem : Item, IUseable
    {
        private Effect Effect;
        public ConsumableItem(int id, string name, string description, Effect effect)
            :base(id, name ,description)
        {
            this.Effect = effect;
        }

        public Effect GetEffect()
        {
            return Effect;
        }

        public Effect Use()
        {
            return Effect;
        }
    }
}