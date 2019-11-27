using Common.Enum;
using Common.Event;
using Elements.Units.UnitCommon;
using UnityEngine;

namespace Elements.Units.Enemies
{
    public class DummyUnit : Unit
    {
        [SerializeField] private UnitData unitData;

        protected override UnitData UnitData
        {
            get => unitData;
            set => unitData = value;
        }

        public override AiInterestCategory InterestCategory => AiInterestCategory.Enemy;

        protected override void DeathVisualEffect()
        {
            // do nothing
        }

        protected override void DeathEffect(DamageSource damageSource)
        {
            EventAggregator.Publish(new EnemyDeadEvent(this, damageSource));
            Destroy(gameObject);
        }
    }
}