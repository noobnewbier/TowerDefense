using Common.Enum;
using Common.Interface;
using Elements.Units.UnitCommon;

namespace Common.Event
{
    public struct EnemyDeadEvent : IDynamicObjectDestroyedEvent
    {
        public EnemyDeadEvent(Unit unit, DamageSource deathCause)
        {
            Unit = unit;
            DeathCause = deathCause;
        }

        public Unit Unit { get; }
        public DamageSource DeathCause { get; }
        public IDynamicObjectOfInterest DynamicObject => Unit;
    }
}