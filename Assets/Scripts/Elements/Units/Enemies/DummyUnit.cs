using Common.Enum;
using Elements.Units.UnitCommon;
using UnityEngine;

namespace Elements.Units.Enemies
{
    //this is a dummy that can potentially fit into anything, if configured correctly
    public class DummyUnit : Enemy
    {
        private IUnitDataRepository _unitDataRepository;
        private IUnitDataModificationService _unitDataModificationService;
        [SerializeField] private UnitProvider provider;
        protected override IUnitDataRepository UnitDataRepository => _unitDataRepository;
        protected override IUnitDataModificationService UnitDataModificationService => _unitDataModificationService;
        protected override void DeathVisualEffect()
        {
            // do nothing
        }

        protected override void Awake()
        {
            base.Awake();

            _unitDataRepository = provider.ProvideUnitDataRepository();
            _unitDataModificationService = provider.ProvideUnitDataService();
        }
    }
}