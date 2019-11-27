using Common.Enum;
using Common.Event;
using Elements.Units.UnitCommon;
using UnityEngine;

namespace Elements.Units.Enemies
{
    public class DummyUnit : Unit
    {
        [field: SerializeField] protected override UnitData UnitData { get; set; }

        public override AiInterestCategory InterestCategory => AiInterestCategory.Enemy;

        protected override void DeathVisualEffect()
        {
            // do nothing
        }

        protected override void DeathEffect(DamageSource damageSource)
        {
            eventAggregator.Publish(new EnemyDeadEvent(this, damageSource));
            Destroy(gameObject);
        }
    }
}