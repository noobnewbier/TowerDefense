using Elements.Units.UnitCommon;
using UnityEngine;

namespace Elements.Units.Enemies.VelocityBased
{
    public class VelocityBasedDataServiceAndRepositoryProvider : MonoBehaviour
    {
        private VelocityBasedUnitData _instance;
        [SerializeField] private VelocityBasedUnitData unitData;

        private VelocityBasedUnitData LazyInstance
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

        public IVelocityBasedUnitDataRepository ProvideUnitDataRepository() => new VelocityBasedUnitDataRepository(LazyInstance);
    }
}