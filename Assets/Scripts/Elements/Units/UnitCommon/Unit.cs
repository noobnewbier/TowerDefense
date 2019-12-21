using System;
using System.Diagnostics;
using Common.Constant;
using Common.Enum;
using Common.Event;
using Common.Interface;
using EventManagement;
using TrainingSpecific;
using UnityEngine;

/*
 * Pooling is not done here, simply because we are just not going to have that many enemies:
 *     1. Its against our design aim - not abusing player with a horrific amount of enemies
 *     2. Pooling it makes the training thing even more complicated
 *
 * So no, no pooling
 */
namespace Elements.Units.UnitCommon
{
    [DefaultExecutionOrder(20)]
    public abstract class Unit : Element, IDamageTaker, IHandle<ForceResetEvent>
    {
        [SerializeField] private Collider unitCollider;
        protected abstract UnitData UnitData { get; set; }
        public override Bounds Bounds => unitCollider.bounds;

        private DamageSource _deadCause;
        private bool _isDyingNextFrame;

        public void Handle(DamageEvent @event)
        {
            if (ReferenceEquals(@event.DamageTaker, this) && !_isDyingNextFrame)
            {
                TakeDamage(@event.Amount, @event.DamageSource);
            }
        }

        protected void Awake()
        {
            UnitData = Instantiate(UnitData);
        }

        protected void TakeDamage(int damage, DamageSource damageSource)
        {
            UnitData.Health -= damage;
            if (UnitData.Health <= 0)
            {
                SetToDieNextFrame(damageSource);
            }
        }

        protected void FixedUpdate()
        {
            if (_isDyingNextFrame)
            {
                Dies(_deadCause);
            }
        }

        private void SetToDieNextFrame(DamageSource deadCause)
        {
            _isDyingNextFrame = true;
            _deadCause = deadCause;
            PublishDeathEvent(deadCause);
        }

        public void Handle(ForceResetEvent @event)
        {
            if (!ReferenceEquals(@event.DynamicObjectOfInterest, this))
            {
                return;
            }
            
            TakeDamage(UnitData.MaxHealth, DamageSource.System);
        }
        
        private void Dies(DamageSource damageSource)
        {
            //order should be important here...
            DeathVisualEffect();
            DeathEffect(damageSource);
        }

        [Conditional(GameConfig.GameplayMode)]
        protected abstract void DeathVisualEffect();

        protected abstract void DeathEffect(DamageSource damageSource);
        protected abstract void PublishDeathEvent(DamageSource deadCause);
    }
}