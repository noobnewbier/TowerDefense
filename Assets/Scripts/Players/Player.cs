using Health;
using UnityEngine;

namespace Players
{
    //perhaps no damage handler afterall...?
    public class Player : MonoBehaviour, IDamageTaker
    {
        [SerializeField] private HealthData healthData;

        public void Handle(DamageEvent @event)
        {
            if (!ReferenceEquals(@event.DamageTaker, this)) return;

            healthData.Health -= @event.Damage;
        }

        HealthData IDamageTaker.HealthData => healthData;
    }
}