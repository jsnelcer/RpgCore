using RpgCore.Inteface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RpgCore.Storaged
{
    public class Storage<T> : IStorage<T>
    {
        public List<T> Items => items;
        private List<T> items { get; set; }

        public Storage()
        {
            this.items = new List<T>();
        }

        public virtual void AddItem(T item)
        {
            if(items == null)
            {
                items = new List<T>();
            }
            items.Add(item);
        }

        public virtual void RemoveItem(T item)
        {
            if (items == null)
            {
                items = new List<T>();
            }
            if (items.Any(x => x.Equals(item)))
            {
                items.Remove(item);
            }
            else
            {
                throw new Exception("item not found");
            }
        }
    }
}
