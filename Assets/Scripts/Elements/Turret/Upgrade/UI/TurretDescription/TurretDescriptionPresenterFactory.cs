using EventManagement;
using UnityEngine;

namespace Elements.Turret.Upgrade.UI.TurretDescription
{
    [CreateAssetMenu(menuName = "ScriptableFactory/TurretDescriptionPresenter")]
    public class TurretDescriptionPresenterFactory : ScriptableObject
    {
        [SerializeField] private IEventAggregator eventAggregator;
        [SerializeField] private TurretDescriptionModel model;

        public ITurretDescriptionPresenter CreatePresenter(ITurretDescriptionView view)
        {
            return new TurretDescriptionPresenter(eventAggregator, view, model);
        }
    }
}