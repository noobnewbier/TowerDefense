using System.Collections.Generic;
using System.Diagnostics;
using Common.Constant;
using Common.Enum;
using Common.Event;
using Common.Interface;
using Effects;
using EventManagement;
using Rules;
using TrainingSpecific.Events;
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
    public abstract class Unit : Element, IEffectTaker, IHandle<ForceResetEvent>
    {
        private IList<EffectHandler> _effectsHandler;
        [SerializeField] private Fact[] facts;
        [SerializeField] private Collider unitCollider;
        public override Bounds Bounds => unitCollider.bounds;
        protected abstract IUnitDataRepository UnitDataRepository { get; }
        protected abstract IUnitDataService UnitDataService { get; }

        public IEnumerable<Fact> Facts => facts;

        public void Handle(ApplyEffectEvent @event)
        {
            if (!ReferenceEquals(@event.EffectTaker, this) || UnitDataService.IsDyingNextFrame)
            {
                return;
            }

            ApplyEffect(@event.Effect, @event.Source);
        }

        public void Handle(ForceResetEvent @event)
        {
            if (!ReferenceEquals(@event.DynamicObjectOfInterest, this))
            {
                return;
            }

            UnitDataService.ModifyHealth(-UnitDataRepository.Health, EffectSource.System);
        }

        protected virtual void Awake()
        {
            _effectsHandler = new List<EffectHandler>();
        }

        protected void FixedUpdate()
        {
            // If it is going to die, die
            if (UnitDataService.IsDyingNextFrame)
            {
                Dies();
            }

            foreach (var handler in _effectsHandler) handler.OnTick();

            // TODO: this looks a bit strange to me... seems weird to be actively asking "am I going to die?" 
            // If it is going to die after applying the effect, publish the event it is going to die 
            if (UnitDataService.IsDyingNextFrame)
            {
                PublishDeathEvent(UnitDataService.DeathSource);
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
        protected abstract void PublishDeathEvent(EffectSource deadCause);

        protected void ApplyEffect(Effect effect, EffectSource source)
        {
            var handler = effect.CreateEffectHandler(UnitDataService, UnitDataRepository, source);
            handler.InitEffect();

            _effectsHandler.Add(handler);
        }
    }
}