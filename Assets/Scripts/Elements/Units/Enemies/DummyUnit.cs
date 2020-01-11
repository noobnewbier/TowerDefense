using Common.Enum;
using Elements.Units.UnitCommon;
using UnityEngine;

namespace Elements.Units.Enemies
{
    //this is a dummy that can potentially fit into anything, if configured correctly
    public class DummyUnit : Enemy
    {
        private IUnitDataRepository _unitDataRepository;
        private IUnitDataService _unitDataService;
        [SerializeField] private UnitDataServiceAndRepositoryProvider provider;
        public override AiInterestCategory InterestCategory => AiInterestCategory.Enemy;
        protected override IUnitDataRepository UnitDataRepository => _unitDataRepository;
        protected override IUnitDataService UnitDataService => _unitDataService;
        protected override void DeathVisualEffect()
        {
            // do nothing
        }

        protected override void Awake()
        {
            base.Awake();

            _unitDataRepository = provider.ProvideUnitDataRepository();
            _unitDataService = provider.ProvideUnitDataService();
        }
    }
}