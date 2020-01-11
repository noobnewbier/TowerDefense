using UnityEngine;

namespace Elements.Units.UnitCommon
{
    public class UnitDataServiceAndRepositoryProvider : MonoBehaviour
    {
        private UnitData _instance;
        [SerializeField] private UnitData unitData;

        private UnitData LazyInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Instantiate(unitData);
                }

                return _instance;
            }
        }

        public IUnitDataService ProvideUnitDataService() => new UnitDataService(LazyInstance);
        public IUnitDataRepository ProvideUnitDataRepository() => new UnitDataRepository(LazyInstance);
    }
}