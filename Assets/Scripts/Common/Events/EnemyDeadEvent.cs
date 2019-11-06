using Units.UnitCommon;

namespace Common.Events
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