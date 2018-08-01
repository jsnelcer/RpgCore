using RpgCore.Enum;

namespace RpgCore.Inteface
{
    public interface IEffect
    {
        EffectTarget Target { get; }
        StatType TargetStat { get; }
        float Value { get; }

        void IncreastValue(float value);
        void ApplyEffect(IStat target);
    }
}