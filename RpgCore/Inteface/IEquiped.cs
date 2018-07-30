using RpgCore.Enum;
namespace RpgCore.Inteface
{
    public interface IEquiped : IItem
    {
        bool Equiped { get; set; }
        EquipSlot Slot { get; }
    }
}
