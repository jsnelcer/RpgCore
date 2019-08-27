using RpgCore;
using RpgCore.Enum;
using RpgCore.Interface;
using System;

namespace RpgCore.Items
{
    public class ConsumableItem : IItem, IUseable<IEffect>, IInteractable
    {
        private int id { get; set; }
        private string description { get; set; }
        private string name { get; set; }

        public int Id => id;
        public string Name => name;
        public string Description => description;

        public IEffect Effect { private set; get; }

        public ConsumableItem(int id, string name, string description, IEffect value)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.Effect = value;
        }

        protected ConsumableItem(ConsumableItem anotherItem)
        {
            this.id = anotherItem.Id;
            this.name = anotherItem.Name;
            this.description = anotherItem.Description;
            this.Effect = anotherItem.Effect.Clone();
        }

        public void SetEffect(IEffect value)
        {
            if(Effect == null)
            {
                this.Effect = value;
            }
        }

        public IEffect Use()
        {
            return Effect;
        }

        public IItem Clone()
        {
            return new ConsumableItem(this);
        }

        public void Interact(ICharacter character)
        {
            character.AddToInventory(this);
        }
    }
}