using Common;
using Common.Events;
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

        protected override void Dies()
        {
            EventAggregator.Publish(new EnemyDeadEvent(this));
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
            var damageTaker = other.gameObject.GetComponent<IDamageTaker>();
            if (damageTaker != null)
            {
                EventAggregator.Publish(new DamageEvent(damageTaker, data.Damage));
                Dies();
            }
        }
    }
}