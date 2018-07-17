using System;
using System.Collections.Generic;
using System.Linq;
using RpgCore.Storaged;
using RpgCore.Enum;


namespace RpgCore
{
    public abstract class Character
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Character(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
        
        public override string ToString() 
        {
            return this.Name + ": " + this.Description;
        }
    }
}
