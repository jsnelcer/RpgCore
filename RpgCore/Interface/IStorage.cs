using System.Collections.Generic;

namespace RpgCore.Interface
{
    public interface IStorage<T>
    {
        void AddItem(T item);
        void RemoveItem(T item);
        
        List<T> Items { get; }
    }
}