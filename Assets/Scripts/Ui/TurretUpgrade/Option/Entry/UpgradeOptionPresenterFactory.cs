using Elements.Turret.Upgrade;
using Experimental;
using ScriptableService;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ui.TurretUpgrade.Option.Entry
{
    [CreateAssetMenu(menuName = "ScriptableFactory/UpgradeOptionPresenter")]
    public class UpgradeOptionPresenterFactory : ScriptableObject
    {
        [FormerlySerializedAs("eventAggregator")] [SerializeField] private EventAggregatorProvider eventAggregatorProvider;
        [SerializeField] private UpgradeOptionModel[] upgradeOptionModels;
        [SerializeField] private SelectedOptionModel selectedOptionModel;
        [SerializeField] private UseResourceService useResourceService;


        public IUpgradeOptionPresenter CreatePresenter(IUpgradeOptionView view, IUpgradable upgradable, int index)
        {
            return new UpgradeOptionPresenter(
                eventAggregatorProvider.ProvideEventAggregator(),
                view,
                upgradeOptionModels[index],
                selectedOptionModel,
                upgradable,
                useResourceService
            );
        }
    }
}