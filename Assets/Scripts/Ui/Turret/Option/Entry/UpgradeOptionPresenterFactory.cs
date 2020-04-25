using Elements.Turret.Upgrade;
using EventManagement;
using EventManagement.Providers;
using Experimental;
using ScriptableService;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ui.Turret.Option.Entry
{
    [CreateAssetMenu(menuName = "ScriptableFactory/UpgradeOptionPresenter")]
    public class UpgradeOptionPresenterFactory : ScriptableObject
    {
        [FormerlySerializedAs("eventAggregator")] [SerializeField] private EventAggregatorProvider eventAggregatorProvider;
        [SerializeField] private UpgradeOptionModel[] upgradeOptionModels;
        [SerializeField] private SelectedOptionModel selectedOptionModel;
        [SerializeField] private UseResourceService useResourceService;
        [SerializeField] private PlaceTurretService placeTurretService;
        
        public IUpgradeOptionPresenter CreatePresenter(IUpgradeOptionView view, IUpgradable upgradable, int index)
        {
            return new UpgradeOptionPresenter(
                eventAggregatorProvider.ProvideEventAggregator(),
                view,
                upgradeOptionModels[index],
                selectedOptionModel,
                upgradable,
                useResourceService,
                placeTurretService
            );
        }
    }
}