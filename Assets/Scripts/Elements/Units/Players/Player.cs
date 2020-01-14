using System;
using Common.Enum;
using Common.Event;
using Elements.Units.UnitCommon;
using UnityEngine;

namespace Elements.Units.Players
{
    public class Player : Unit
    {
        private IUnitDataRepository _unitDataRepository;
        private IUnitDataModificationService _unitDataModificationService;
        [SerializeField] private UnitProvider provider;

        protected override IUnitDataRepository UnitDataRepository => _unitDataRepository;
        protected override IUnitDataModificationService UnitDataModificationService => _unitDataModificationService;

        public override AiInterestCategory InterestCategory => AiInterestCategory.Player;

        protected override void OnEnable()
        {
            base.OnEnable();
            _unitDataRepository = provider.ProvideUnitDataRepository();
            _unitDataModificationService = provider.ProvideUnitDataService();

            EventAggregator.Publish(new PlayerSpawnedEvent(this));
        }
        
        protected override void DeathVisualEffect()
        {
            //todo: not implemented
        }

        protected override void DeathEffect()
        {
            Destroy(gameObject);
        }

        protected override void PublishDeathEvent(EffectSource deadCause)
        {
            EventAggregator.Publish(new PlayerDeadEvent(this));
        }
    }
}