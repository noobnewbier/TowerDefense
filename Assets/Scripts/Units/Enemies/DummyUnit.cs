using Bullets;
using Common;
using Units.UnitCommon;
using UnityEngine;

namespace Units.Enemies
{
    public class DummyUnit : Unit
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(ObjectTags.DamageAi))
            {
                TakeDamage(other.GetComponent<Bullet>().Damage);
            }
        }
    }
}