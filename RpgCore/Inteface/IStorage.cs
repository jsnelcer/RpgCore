using System.Collections.Generic;

interface IStorage<T>
{
    void AddItem(T item);
    void RemoveItem(T item);
    List<T> GetItems();
}