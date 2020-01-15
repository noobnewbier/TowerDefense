using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        private List<EffectHandler> _effectsHandlers;
        [SerializeField] private Collider unitCollider;
        public override Bounds Bounds => unitCollider.bounds;
        protected abstract IUnitDataRepository UnitDataRepository { get; }
        protected abstract IUnitDataModificationService UnitDataModificationService { get; }

        public IEnumerable<Fact> Facts => UnitDataRepository.Facts;

        public void Handle(ApplyEffectEvent @event)
        {
            if (!ReferenceEquals(@event.EffectTaker, this) || UnitDataModificationService.IsDyingNextFrame)
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

            UnitDataModificationService.ModifyHealth(-UnitDataRepository.Health, EffectSource.System);
        }

        protected virtual void Awake()
        {
            _effectsHandlers = new List<EffectHandler>();
        }

        protected void FixedUpdate()
        {
            // If it is going to die, die
            if (UnitDataModificationService.IsDyingNextFrame)
            {
                Dies();
            }

            UpdateEffects();

            // TODO: this looks a bit strange to me... seems weird to be actively asking "am I going to die?" 
            // If it is going to die after applying the effect, publish the event it is going to die 
            if (UnitDataModificationService.IsDyingNextFrame)
            {
                PublishDeathEvent(UnitDataModificationService.DeathSource);
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
            var handler = effect.CreateEffectHandler(UnitDataModificationService, UnitDataRepository, source);
            var canApply = handler.TryInitEffect(_effectsHandlers.Select(h => h.Effect));

            if (canApply)
            {
                _effectsHandlers.Add(handler);
            }
        }

        private void UpdateEffects()
        {
            foreach (var handler in _effectsHandlers) handler.OnTick(Time.fixedDeltaTime);

            _effectsHandlers.RemoveAll(h => h.IsDone);
        }
    }
}