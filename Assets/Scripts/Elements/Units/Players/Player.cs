using Common.Enum;
using Common.Event;
using Elements.Units.UnitCommon;
using Interactable;
using UnityEngine;

namespace Elements.Units.Players
{
    public class Player : Unit, ICanInteract
    {
        [SerializeField] private UnitData unitData;

        protected override UnitData UnitData
        {
            get => unitData;
            set => unitData = value;
        }

        public override AiInterestCategory InterestCategory => AiInterestCategory.Player;

        protected override void OnEnable()
        {
            base.OnEnable();

            EventAggregator.Publish(new PlayerSpawnedEvent(this));
        }

        protected override void DeathVisualEffect()
        {
            //todo: not implemented
        }

        protected override void DeathEffect(DamageSource damageSource)
        {
            Destroy(gameObject);
        }

        protected override void PublishDeathEvent(DamageSource deadCause)
        {
            EventAggregator.Publish(new PlayerDeadEvent(this));
        }

        public void Handle(ApplyEffectEvent @event)
        {
            @event.Effect.ApplyEffect(unitData);
        }
    }
}