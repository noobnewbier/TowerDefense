using Common.Enum;
using Common.Event;
using Elements.Units.UnitCommon;
using UnityEngine;

namespace Elements.Units.Players
{
    public class Player : Unit
    {
        [field: SerializeField] protected override UnitData UnitData { get; set; }

        public override AiInterestCategory InterestCategory => AiInterestCategory.Player;


        protected override void DeathVisualEffect()
        {
            //not implemented
        }

        protected override void DeathEffect(DamageSource damageSource)
        {
            eventAggregator.Publish(new PlayerDeadEvent());
            Destroy(gameObject);
            //not implemented
        }
    }
}