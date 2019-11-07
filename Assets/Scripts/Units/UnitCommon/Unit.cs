using Common;
using Common.Events;
using EventManagement;
using UnityEngine;

/*
 * Pooling is not done here, simply because we are just not going to have that many enemies:
 *     1. Its against our design aim - not abusing player with a horrific amount of enemies
 *     2. Pooling it makes the training thing even more complicated
 *
 * So no, no pooling
 */
namespace Units.UnitCommon
{
    public abstract class Unit : MonoBehaviour, IDamageTaker
    {
        protected IEventAggregator EventAggregator;
        protected abstract UnitData UnitData { get; set; }
        public Transform Transform => transform;

        public void Handle(DamageEvent @event)
        {
            if (ReferenceEquals(@event.DamageTaker, this))
            {
                TakeDamage(@event.Amount);
            }
        }

        protected void Awake()
        {
            UnitData = Instantiate(UnitData);
        }

        private void OnEnable()
        {
            EventAggregator = EventAggregatorHolder.Instance;
            EventAggregator.Subscribe(this);
        }

        protected virtual void TakeDamage(int damage)
        {
            UnitData.Health -= damage;
            if (UnitData.Health <= 0)
            {
                Dies();
            }
        }

        protected abstract void Dies();

        private void OnDisable()
        {
            EventAggregator.Unsubscribe(this);
        }
    }
}