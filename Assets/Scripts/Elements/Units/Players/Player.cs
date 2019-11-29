using Common.Enum;
using Common.Event;
using Elements.Units.UnitCommon;
using TrainingSpecific;
using UnityEngine;

namespace Elements.Units.Players
{
    public class Player : Unit
    {
        [SerializeField] private UnitData unitData;

        protected override UnitData UnitData
        {
            get => unitData;
            set => unitData = value;
        }

        public override AiInterestCategory InterestCategory => AiInterestCategory.Player;
        protected override void DeathVisualEffect()
        {
            //not implemented
        }

        protected override void DeathEffect(DamageSource damageSource)
        {
            EventAggregator.Publish(new PlayerDeadEvent());
            Destroy(gameObject);
        }
    }
}