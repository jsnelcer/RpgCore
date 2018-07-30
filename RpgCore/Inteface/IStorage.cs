using System.Collections.Generic;

namespace RpgCore.Inteface
{
    public interface IStorage<T>
    {
        void AddItem(T item);
        void RemoveItem(T item);
        List<T> Items { get; }
    }
}