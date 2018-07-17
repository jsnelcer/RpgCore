using RpgCore.Enum;
namespace RpgCore.Stats
{
    public class BaseStat
    {
        public StatType Type { get; private set; }
        protected float Value { get; set; }
        public BaseStat(float value, StatType type)
        {
            this.Type = type;
            this.Value = value;
        }

        public virtual float GetValue()
        {
            return Value;
        }
    }
}
