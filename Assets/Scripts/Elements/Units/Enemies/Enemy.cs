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

        protected override void DeathEffect(DamageSource damageSource)
        {
            Destroy(gameObject);
        }

        protected override void PublishDeathEvent(DamageSource deadCause)
        {
            EventAggregator.Publish(new EnemyDeadEvent(this, deadCause));
        }
    }
}