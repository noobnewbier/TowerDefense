using EventManagement;
using EventManagement.Providers;
using Experimental;
using Ui.Turret.Option.Entry;
using Ui.Turret.Option.ListView;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ui.Turret.Option
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