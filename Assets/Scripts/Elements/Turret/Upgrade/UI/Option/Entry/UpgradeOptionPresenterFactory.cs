using Experimental;
using UnityEngine;
using UnityEngine.Serialization;

namespace Elements.Turret.Upgrade.UI.Option.Entry
{
    [CreateAssetMenu(menuName = "ScriptableFactory/UpgradeOptionPresenter")]
    public class UpgradeOptionPresenterFactory : ScriptableObject
    {
        [FormerlySerializedAs("eventAggregator")] [SerializeField] private EventAggregatorProvider eventAggregatorProvider;
        [SerializeField] private UpgradeOptionModel[] upgradeOptionModels;
        [SerializeField] private SelectedOptionModel selectedOptionModel;
        

        public IUpgradeOptionPresenter CreatePresenter(IUpgradeOptionView view, IUpgradable upgradable, int index)
        {
            return new UpgradeOptionPresenter(eventAggregatorProvider.ProvideEventAggregator(), view, upgradeOptionModels[index], selectedOptionModel, upgradable);
        }
        
    }
}