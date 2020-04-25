using Elements.Turret.Upgrade;
using EventManagement;
using EventManagement.Providers;
using Experimental;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ui.Turret.Option.ListView
{
    [CreateAssetMenu(menuName = "ScriptableFactory/UpgradeOptionListPresenter")]
    public class UpgradeOptionListPresenterFactory : ScriptableObject
    {
        [FormerlySerializedAs("eventAggregator")] [SerializeField] private EventAggregatorProvider eventAggregatorProvider;
        [SerializeField] private UpgradeOptionsListModel model;
        [SerializeField] private TurretUpgradableQueriesService upgradableQueriesService;

        public IUpgradeOptionListPresenter CreatePresenter(IUpgradeOptionsListView view, IUpgradable upgradable) =>
            new UpgradeOptionListPresenter(
                eventAggregatorProvider.ProvideEventAggregator(),
                model,
                view,
                upgradableQueriesService,
                upgradable
            );
    }
}