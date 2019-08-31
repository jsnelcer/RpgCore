using RpgCore.Enum;
using System.Collections.Generic;
namespace RpgCore.Interface
{
    public interface IEquiped : IItem
    {
        List<IEffect> EquipEffects { get; }
        bool Equiped { get; set; }
        EquipSlot Slot { get; }
    }
}
