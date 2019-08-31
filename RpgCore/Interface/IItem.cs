namespace RpgCore.Interface
{
    public interface IItem : IEntity, IInteractable
    {
        IItem Clone();
    }
}
