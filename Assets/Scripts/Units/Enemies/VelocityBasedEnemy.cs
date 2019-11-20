using Common;
using Common.Constant;
using Common.Enum;
using Common.Event;
using Common.Interface;
using Units.Enemies.Data;
using Units.UnitCommon;
using UnityEngine;

namespace Units.Enemies
{
    public class VelocityBasedEnemy : Unit, IHasRotation, IHasAcceleration
    {
        [SerializeField] private VelocityBasedUnitData data;

        protected override UnitData UnitData
        {
            get => data;
            set => data = (VelocityBasedUnitData) value;
        }

        public float Acceleration => data.Acceleration;

        public float RotationSpeed => data.RotationSpeed;


        protected override void DeathEffect()
        {
            EventAggregator.Publish(new EnemyDeadEvent(this));
            Destroy(gameObject);
        }

        public override AiInterestedObjectType InterestedObjectType => AiInterestedObjectType.Enemy;

        protected override void DeathVisualEffect()
        {
            // do nothing for now
        }

        private void OnCollisionEnter(Collision other)
        {
            var damageTaker = other.gameObject.GetComponent<IDamageTaker>();
            if (damageTaker != null && other.collider.CompareTag(ObjectTags.Player))
            {
                EventAggregator.Publish(new DamageEvent(damageTaker, data.Damage, DamageSource.Ai));
                TakeDamage(data.MaxHealth, DamageSource.SelfDestruction);
            }
        }
    }
}