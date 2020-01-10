using System.Linq;
using Common.Enum;
using Common.Event;
using Common.Interface;
using Effects;
using Elements.Units.UnitCommon;
using Rules;
using UnityEngine;

namespace Elements.Units.Enemies
{
    //enemy that touches you and explode, deals damage and destruct itself
    public class SuicidalEnemy : Enemy
    {
        private IUnitDataRepository _unitDataRepository;
        private IUnitDataService _unitDataService;
        [SerializeField] private UnitDataServiceAndRepositoryProvider provider;
        [SerializeField] private Effect damageEffect;
        [SerializeField] private Effect selfEffectWhenCollide;
        [SerializeField] private Rule[] rules;

        public override AiInterestCategory InterestCategory => AiInterestCategory.Enemy;
        protected override IUnitDataRepository UnitDataRepository => _unitDataRepository;
        protected override IUnitDataService UnitDataService => _unitDataService;

        protected override void Awake()
        {
            base.Awake();

            _unitDataRepository = provider.ProvideUnitDataRepository();
            _unitDataService = provider.ProvideUnitDataService();
        }

        protected override void DeathVisualEffect()
        {
            // do nothing for now
        }

        private void OnCollisionEnter(Collision other)
        {
            var effectTaker = other.gameObject.GetComponent<IEffectTaker>();
            if (effectTaker != null && rules.All(r => r.AdhereToRule(effectTaker)))
            {
                EventAggregator.Publish(new ApplyEffectEvent(damageEffect, effectTaker, EffectSource.Ai));
                ApplyEffect(selfEffectWhenCollide, EffectSource.SelfDestruction);
            }
        }
    }
}