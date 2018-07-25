using RpgCore.Enum;

namespace RpgCore.Inteface
{
    public interface IEffect<T>
    {
        StatType GetTargetStat();
        void ApplyEffect(T target);
    }
}
