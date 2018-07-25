using System;
using System.Collections.Generic;
using RpgCore.Enum;
using RpgCore.Inteface;
using RpgCore.Items;

namespace RpgCore.Items
{
    public class Resources : Item, IUseable
    {
        public Resources(int id, string name, string description) 
            : base(id, name, description)
        {
        }

        public Effect Use()
        {
            throw new NotImplementedException();
        }
    }
}
