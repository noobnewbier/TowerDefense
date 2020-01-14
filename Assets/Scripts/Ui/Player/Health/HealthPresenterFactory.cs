using Elements.Units.UnitCommon;
using Experimental;
using UnityEngine;

namespace Ui.Player.Health
{
    [CreateAssetMenu(menuName = "ScriptableFactory/HealthPresenter")]
    public class HealthPresenterFactory : ScriptableObject
    {
        [SerializeField] private EventAggregatorProvider eventAggregatorProvider;
        
        public IHealthPresenter CreatePresenter(IHealthView view, IUnitDataRepository repository, Unit unit)
        {
            return new HealthPresenter(repository, unit, view, eventAggregatorProvider.ProvideEventAggregator());
        }
    }
}