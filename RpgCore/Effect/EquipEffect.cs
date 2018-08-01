using RpgCore.Enum;
using RpgCore.Inteface;

namespace RpgCore
{
    public class EquipEffect : IEffect
    {
        public EffectTarget Target => throw new System.NotImplementedException();

        public StatType TargetStat => throw new System.NotImplementedException();

        public float Value => throw new System.NotImplementedException();

        public void ApplyEffect(IStat target)
        {
            throw new System.NotImplementedException();
        }

        public void IncreastValue(float value)
        {
            throw new System.NotImplementedException();
        }
    }
}
