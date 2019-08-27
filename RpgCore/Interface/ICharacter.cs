using System.Collections.Generic;

namespace RpgCore.Interface
{
    public interface ICharacter
    {
        string Name { get; }
        string Description { get; }

        List<IInteractable> LookAround();
        void AddToInventory(IItem item);
    }
}
