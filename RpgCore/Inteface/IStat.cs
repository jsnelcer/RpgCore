using RpgCore.Enum;

namespace RpgCore.Inteface
{
    public interface IStat
    {
        StatType Type { get; }
        float Value { get; }

        void ApplyInstantEffect(InstantEffect effectValue);

        void DurationEffectStep(TimeEffect effectValue);
        void DurationEffectEnd(TimeEffect effectValue);
    }
}
