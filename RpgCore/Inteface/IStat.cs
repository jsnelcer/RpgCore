using RpgCore.Enum;
using System.Collections.Generic;     

namespace RpgCore.Inteface
{
    public interface IStat
    {
        StatType Type { get; }
        float Value { get; }
        List<IEffect> Modifiers { get; }

        void ApplyInstantEffect(InstantEffect effectValue);
        void DurationEffectStep(TimeEffect effectValue);
        void DurationEffectEnd(TimeEffect effectValue);
        void EquipEffect(EquipEffect effectValue);
        void RemoveEquipEffects();
    }
}
