using EventManagement;
using EventManagement.Providers;
using Experimental;
using Ui.Turret.Option;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ui.Turret.Description
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