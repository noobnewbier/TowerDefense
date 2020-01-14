using Experimental;
using Ui.TurretUpgrade.Option;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ui.TurretUpgrade.TurretStatistic
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