using Experimental;
using UnityEngine;

namespace Elements.Units.UnitCommon
{
    public class UnitProvider : MonoBehaviour
    {
        private UnitData _dataInstance;

        [SerializeField] private EventAggregatorProvider eventAggregatorProvider;
        [SerializeField] private Unit unit;
        [SerializeField] private UnitData unitData;

        private UnitData LazyData
        {
            get
            {
                if (_dataInstance == null) _dataInstance = Instantiate(unitData);

                return _dataInstance;
            }
        }

        public Unit ProvideUnit() => unit;

        public IUnitDataModificationService ProvideUnitDataService() => new UnitDataModificationModificationService(
            LazyData,
            eventAggregatorProvider.ProvideEventAggregator(),
            unit
        );

        public IUnitDataRepository ProvideUnitDataRepository() => new UnitDataRepository(LazyData);
    }
}