using System;
using System.Collections.Generic;
using System.Linq;

namespace RpgCore.Storaged
{
    public abstract class Storage<T> : IStorage<T>
    {
        public List<T> Items { protected set; get; }
        
        public virtual void AddItem(T item)
        {
            if(Items==null)
            {
                Items = new List<T>();
            }
            Items.Add(item);
        }

        public virtual void RemoveItem(T item)
        {
            if (Items == null)
            {
                Items = new List<T>();
            }
            if (Items.Any(x => x.Equals(item)))
            {
                Items.Remove(item);
            }
            else
            {
                throw new Exception("item not found");
            }
        }

        public List<T> GetItems()
        {
            if (Items==null)
            {
                Items = new List<T>();
            } 
            return Items;
        }

        public override string ToString()
        {
            string result = "";
            if (Items.Any())
            {
                result += "Items:";
                foreach (T x in Items)
                {
                    result += "\n " + x.ToString();
                }
            }

            return result;
        }
    }
}
