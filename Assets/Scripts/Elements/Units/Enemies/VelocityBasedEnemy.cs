using Common.Constant;
using Common.Enum;
using Common.Event;
using Common.Interface;
using Elements.Units.Enemies.Data;
using Elements.Units.UnitCommon;
using UnityEngine;

namespace Elements.Units.Enemies
{
    public class VelocityBasedEnemy : Unit, IHasRotation, IHasAcceleration
    {
        [SerializeField] private VelocityBasedUnitData data;

        protected override UnitData UnitData
        {
            get => data;
            set => data = (VelocityBasedUnitData) value;
        }

        public override AiInterestCategory InterestCategory => AiInterestCategory.Enemy;

        public float Acceleration => data.Acceleration;

        public float RotationSpeed => data.RotationSpeed;


        protected override void DeathEffect(DamageSource damageSource)
        {
            eventAggregator.Publish(new EnemyDeadEvent(this, damageSource));
            Destroy(gameObject);
        }

        protected override void DeathVisualEffect()
        {
            // do nothing for now
        }

        private void OnCollisionEnter(Collision other)
        {
            var damageTaker = other.gameObject.GetComponent<IDamageTaker>();
            if (damageTaker != null && other.collider.CompareTag(ObjectTags.Player))
            {
                eventAggregator.Publish(new DamageEvent(damageTaker, data.Damage, DamageSource.Ai));
                TakeDamage(data.MaxHealth, DamageSource.SelfDestruction);
            }
        }
    }
}