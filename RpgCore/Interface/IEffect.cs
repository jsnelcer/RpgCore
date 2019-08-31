using RpgCore.Enum;

namespace RpgCore.Interface
{
    public interface IEffect
    {
        EffectTarget Target { get; }
        StatType TargetStat { get; }
        float Value { get; }

        void IncreastValue(float value);
        void ApplyEffect(IStat target);

        IEffect Clone();
    }
}