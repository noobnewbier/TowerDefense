using System.Diagnostics;
using Common.Class;
using Common.Constant;
using Common.Enum;
using Common.Event;
using Common.Interface;
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
    [DefaultExecutionOrder (20)]
    public abstract class Unit : MonoBehaviour, IDamageTaker, IObjectOfInterest
    {
        [SerializeField] private Collider unitCollider;
        protected IEventAggregator EventAggregator;
        protected abstract UnitData UnitData { get; set; }
        public Transform Transform => transform;

        public bool IsDead { get; private set; }
        
        public DamageSource? DeathCause { get; private set; }

        public void Handle(DamageEvent @event)
        {
            if (ReferenceEquals(@event.DamageTaker, this))
            {
                TakeDamage(@event.Amount, @event.DamageSource);
            }
        }

        protected void Awake()
        {
            UnitData = Instantiate(UnitData);
        }

        private void FixedUpdate()
        {
            if (IsDead)
            {
                Dies();
            }
        }

        private void OnEnable()
        {
            EventAggregator = EventAggregatorHolder.Instance;
            EventAggregator.Subscribe(this);
        }

        protected void TakeDamage(int damage, DamageSource damageSource)
        {
            UnitData.Health -= damage;
            if (UnitData.Health <= 0)
            {
                IsDead = true;
                DeathCause = damageSource;
            }
        }

        private void Dies()
        {
            //order should be important here...
            DeathVisualEffect();
            DeathEffect();
        }
        
        [Conditional(GameConfig.GameplayMode)]
        protected abstract void DeathVisualEffect();
        protected abstract void DeathEffect();

        private void OnDisable()
        {
            EventAggregator.Unsubscribe(this);
        }

        public abstract AiInterestCategory InterestCategory { get; }
        public Bounds Bounds => unitCollider.bounds;
    }
}