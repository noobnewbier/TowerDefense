using System;
using Common;
using Common.Events;
using EventManagement;
using UnityEngine;

namespace Units.UnitCommon
{
    public abstract class Unit : MonoBehaviour, IDamageTaker
    {
        protected IEventAggregator EventAggregator;
        [SerializeField] protected UnitData unitData;
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
            unitData = Instantiate(unitData);
        }

        private void OnEnable()
        {
            EventAggregator = EventAggregatorHolder.Instance;
            EventAggregator.Subscribe(this);
        }

        protected virtual void TakeDamage(int damage)
        {
            unitData.Health -= damage;
            if (unitData.Health <= 0)
            {
                Dies();
            }
        }

        protected virtual void Dies()
        {
            throw new NotImplementedException();
        }

        private void OnDisable()
        {
            EventAggregator.Unsubscribe(this);
        }
    }
}