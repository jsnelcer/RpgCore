using System;
namespace RpgCore.Inteface
{
    public interface IItem
    {
        int Id { get; }
        string Name { get; }
        string Description { get; }

        IItem Clone();
    }
}
