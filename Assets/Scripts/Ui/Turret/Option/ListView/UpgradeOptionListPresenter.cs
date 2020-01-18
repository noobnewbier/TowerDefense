using System;
using Elements.Turret.Upgrade;
using EventManagement;

namespace Ui.Turret.Option.ListView
{
    public interface IUpgradeOptionListPresenter : IDisposable
    {
        void OnInitialize();
    }

    public class UpgradeOptionListPresenter : IUpgradeOptionListPresenter
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IUpgradeOptionListModel _model;
        private readonly ITurretUpgradableQueriesService _turretUpgradableQueriesService;
        private readonly IUpgradable _upgradable;
        private readonly IUpgradeOptionsListView _view;

        public UpgradeOptionListPresenter
        (
            IEventAggregator eventAggregator,
            IUpgradeOptionListModel model,
            IUpgradeOptionsListView view,
            ITurretUpgradableQueriesService turretUpgradableQueriesService,
            IUpgradable upgradable
        )
        {
            _eventAggregator = eventAggregator;
            _model = model;
            _view = view;
            _turretUpgradableQueriesService = turretUpgradableQueriesService;
            _upgradable = upgradable;

            _eventAggregator.Subscribe(this);
        }

        public void Dispose()
        {
            _eventAggregator.Unsubscribe(this);
        }

        public void OnInitialize()
        {
            _model.SetUpgradableEntries(_turretUpgradableQueriesService.GetUpgradables(_upgradable));
        }
    }
}