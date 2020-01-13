using Elements.Turret.Upgrade.UI.Option;
using Experimental;
using UnityEngine;
using UnityEngine.Serialization;

namespace Elements.Turret.Upgrade.UI.TurretDescription
{
    [CreateAssetMenu(menuName = "ScriptableFactory/TurretDescriptionPresenter")]
    public class TurretDescriptionPresenterFactory : ScriptableObject
    {
        [FormerlySerializedAs("eventAggregator")] [SerializeField] private EventAggregatorProvider eventAggregatorProvider;
        [SerializeField] private SelectedOptionModel model;

        public ITurretDescriptionPresenter CreatePresenter(ITurretDescriptionView view)
        {
            return new TurretDescriptionPresenter(eventAggregatorProvider.ProvideEventAggregator(), view, model);
        }
    }
}