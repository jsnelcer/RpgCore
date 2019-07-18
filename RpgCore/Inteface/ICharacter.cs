using System.Collections.Generic;

namespace RpgCore.Inteface
{
    public interface ICharacter
    {
        string Name { get; }
        string Description { get; }

        List<IInteractable> LookAround();
        void AddToInventory(IItem item);
    }
}
