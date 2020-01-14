using Elements.Turret.Upgrade.UI.Option.Entry;
using Elements.Turret.Upgrade.UI.Option.ListView;
using Experimental;
using UnityEngine;
using UnityEngine.Serialization;

namespace Elements.Turret.Upgrade.UI.Option
{
    public interface ISelectOptionService
    {
        void SelectOption(IUpgradeOptionModel optionModel);
    }

    public interface ISelectedOptionRepository
    {
        IUpgradeOptionModel SelectedUpgradeOptionModel { get; }
    }

    public interface ISelectedOptionModel : ISelectOptionService, ISelectedOptionRepository
    {
    }
    
    [CreateAssetMenu(menuName = "ScriptableModel/SelectedOptionModel")]
    public class SelectedOptionModel : ScriptableObject, ISelectedOptionModel
    {
        [FormerlySerializedAs("eventAggregator")] [SerializeField] private EventAggregatorProvider eventAggregatorProvider;
        
        public void SelectOption(IUpgradeOptionModel optionModel)
        {
            SelectedUpgradeOptionModel = optionModel;
            
            eventAggregatorProvider.ProvideEventAggregator().Publish(new SelectUpgradeOptionEvent(SelectedUpgradeOptionModel));
        }

        public IUpgradeOptionModel SelectedUpgradeOptionModel { get; private set; }
    }
}