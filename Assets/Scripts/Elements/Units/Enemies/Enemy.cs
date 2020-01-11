using Common.Enum;
using Common.Event;
using Elements.Units.UnitCommon;

namespace Elements.Units.Enemies
{
    public abstract class Enemy : Unit
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            EventAggregator.Publish(new EnemySpawnedEvent(this));
        }

        protected override void DeathEffect()
        {
            Destroy(gameObject);
        }

        protected override void PublishDeathEvent(EffectSource deadCause)
        {
            EventAggregator.Publish(new EnemyDeadEvent(this, deadCause));
        }
    }
}