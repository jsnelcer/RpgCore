using System;
using System.Collections.Generic;
using System.Linq;

namespace RpgCore.Storaged
{
    public abstract class Storage<T> : IStorage<T>
    {
        public static List<T> items { protected set; get; }
        
        public virtual void AddItem(T item)
        {
            if(items==null)
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

        public List<T> GetItems()
        {
            if (items==null)
            {
                items = new List<T>();
            } 
            return items;
        }

        public override string ToString()
        {
            string result = "";
            if (items.Any())
            {
                result += "Items:";
                foreach (T x in items)
                {
                    result += "\n " + x.ToString();
                }
            }

            return result;
        }
    }
}
