using Common.Enum;
using Common.Event;
using Elements.Units.UnitCommon;
using UnityEngine;

namespace Elements.Units.Players
{
    public class Player : Unit
    {
        private IUnitDataRepository _unitDataRepository;
        private IUnitDataService _unitDataService;
        [SerializeField] private UnitDataServiceAndRepositoryProvider provider;

        protected override IUnitDataRepository UnitDataRepository => _unitDataRepository;
        protected override IUnitDataService UnitDataService => _unitDataService;

        public override AiInterestCategory InterestCategory => AiInterestCategory.Player;

        protected override void OnEnable()
        {
            base.OnEnable();
            _unitDataRepository = provider.ProvideUnitDataRepository();
            _unitDataService = provider.ProvideUnitDataService();

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