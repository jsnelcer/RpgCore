using System;
namespace RpgCore.Inteface
{
    public interface IItem : ICloneable
    {
        int Id { get; }
        string Name { get; }
        string Description { get; }

        IItem Copy();
    }
}
