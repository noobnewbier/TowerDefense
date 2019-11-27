using System.Diagnostics;
using Common.Constant;
using Common.Enum;
using Common.Event;
using Common.Interface;
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
    public abstract class Unit : Element, IDamageTaker
    {
        [SerializeField] private Collider unitCollider;
        protected abstract UnitData UnitData { get; set; }
        public Transform Transform => transform;
        public override Bounds Bounds => unitCollider.bounds;

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

        protected void TakeDamage(int damage, DamageSource damageSource)
        {
            UnitData.Health -= damage;
            if (UnitData.Health <= 0)
            {
                Dies(damageSource);
            }
        }

        //todo : make sure the death mechanism works with the agent as well...
        private void Dies(DamageSource damageSource)
        {
            //order should be important here...
            DeathVisualEffect();
            DeathEffect(damageSource);
        }

        [Conditional(GameConfig.GameplayMode)]
        protected abstract void DeathVisualEffect();

        protected abstract void DeathEffect(DamageSource damageSource);
    }
}