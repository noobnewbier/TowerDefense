using Units.UnitCommon;

namespace Common.Event
{
    public struct EnemyDeadEvent
    {
        public EnemyDeadEvent(Unit unit)
        {
            Unit = unit;
        }

        public Unit Unit { get; }
    }
}