using Common.Events;
using Units.UnitCommon;

namespace Units.Enemies
{
    public class DummyUnit : Unit
    {
        protected override void Dies()
        {
            EventAggregator.Publish(new EnemyDeadEvent(this));
            Destroy(gameObject);
        }
    }
}