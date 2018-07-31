using RpgCore.Enum;

namespace RpgCore.Inteface
{
    public interface IEffect<T>
    {
        EffectTarget Target { get; }
        StatType TargetStat { get; }
        float Value { get; }

        void IncreastValue(float value);
        void ApplyEffect(T target);
    }
}
