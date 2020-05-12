using Common.Enum;

namespace Elements.Units.UnitCommon
{
    public struct UnitHealthChangedEvent
    {
        public UnitHealthChangedEvent(Unit unitChanged, EffectSource effectSource, float amount)
        {
            UnitChanged = unitChanged;
            EffectSource = effectSource;
            Amount = amount;
        }

        public Unit UnitChanged { get; }
        public EffectSource EffectSource { get; }
        public float Amount { get; }
    }
}