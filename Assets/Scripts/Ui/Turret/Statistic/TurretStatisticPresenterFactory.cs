using Experimental;
using Ui.Turret.Option;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ui.Turret.Statistic
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