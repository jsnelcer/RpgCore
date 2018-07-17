using System;
using System.Collections.Generic;
using System.Linq;
using RpgCore.Items;

namespace RpgCore.Storaged
{
    public sealed class Inventory : Storage<Item>
    {
        //private static Inventory instance = null;
        /*
        public static Inventory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Inventory();
                }
                return instance;
            }           
        }*/

        public Inventory()
        {

        }
        

        public void Use(ConsumableItem item)
        {

        }
    }
}
