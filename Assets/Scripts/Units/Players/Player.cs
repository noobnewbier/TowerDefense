using Bullets;
using Common;
using Common.Events;
using Units.UnitCommon;
using UnityEngine;

namespace Units.Players
{
    public class Player : Unit
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(ObjectTags.DamagePlayer))
            {
                TakeDamage(other.GetComponent<Bullet>().Damage);
            }
        }

        protected override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);

            if (unitData.Health <= 0)
            {
                EventAggregatorHolder.Instance.Publish(new PlayerDeadEvent());
            }
        }
    }
}