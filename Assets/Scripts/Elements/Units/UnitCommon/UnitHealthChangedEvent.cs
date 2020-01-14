namespace Elements.Units.UnitCommon
{
    public struct UnitHealthChangedEvent
    {
        public UnitHealthChangedEvent(Unit unitChanged)
        {
            UnitChanged = unitChanged;
        }

        public Unit UnitChanged { get; }
    }
}