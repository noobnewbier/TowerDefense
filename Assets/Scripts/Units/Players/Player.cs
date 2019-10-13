using Bullets;
using Common;
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
    }
}    