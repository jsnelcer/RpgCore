using System.Collections.Generic;

namespace RpgCore.Inteface
{
    public interface ICharacter : IEntity
    {
        IStorage<IItem> Inventory { get; }
        List<IEntity> LookAround();
    }
}
