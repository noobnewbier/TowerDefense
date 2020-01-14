using Elements.Turret.Upgrade.UI.Option;
using Experimental;
using UnityEngine;
using UnityEngine.Serialization;

namespace Elements.Turret.Upgrade.UI.TurretStatistic
{
    [CreateAssetMenu(menuName = "ScriptableFactory/TurretStatisticPresenterFactory")]
    public class TurretStatisticPresenterFactory : ScriptableObject
    {
        [SerializeField] private SelectedOptionModel model;
        [FormerlySerializedAs("eventAggregator")] [SerializeField] private EventAggregatorProvider eventAggregatorProvider;
        
        public IStatisticPresenter CreatePresenter(IStatisticView view)
        {
            return new TurretStatisticPresenter(view, model, eventAggregatorProvider.ProvideEventAggregator());
        } 
    }
}