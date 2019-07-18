using System;
using System.Collections.Generic;
using RpgCore.Enum;
using RpgCore.Inteface;
using RpgCore.Items;

namespace RpgCore.Items
{
    public class Resources : IItem, IInteractable
    {

        private int id { get; set; }
        private string description { get; set; }
        private string name { get; set; }

        public int Id => id;
        public string Name => name;
        public string Description => description;

        public Resources(int id, string name, string description) 
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }

        protected Resources(Resources anotherItem)
        {
            this.id = anotherItem.Id;
            this.name = anotherItem.Name;
            this.description = anotherItem.Description;
        }

        public IItem Clone()
        {
            return new Resources(this);
        }

        public void Interact(ICharacter character)
        {
            character.AddToInventory(this);
        }
    }
}
