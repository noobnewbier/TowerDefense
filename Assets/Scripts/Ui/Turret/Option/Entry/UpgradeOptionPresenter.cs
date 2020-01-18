using System;
using Elements.Turret.Upgrade;
using EventManagement;
using ScriptableService;

namespace Ui.Turret.Option.Entry
{
    public interface IUpgradeOptionPresenter : IDisposable
    {
        void OnCheckOption();
        void OnSelectOption();
    }

    public class UpgradeOptionPresenter : IUpgradeOptionPresenter, IHandle<UpgradeOptionModelUpdateEvent>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IUpgradeOptionView _view;
        private readonly IUpgradeOptionModel _model;
        private readonly ISelectedOptionModel _selectedOptionModel;
        private readonly IUpgradable _upgradable;
        private readonly IUseResourceService _useResourceService;

        public UpgradeOptionPresenter
        (
            IEventAggregator eventAggregator,
            IUpgradeOptionView view,
            IUpgradeOptionModel model,
            ISelectedOptionModel selectedOptionModel,
            IUpgradable upgradable,
            IUseResourceService useResourceService
        )
        {
            _eventAggregator = eventAggregator;
            _view = view;
            _model = model;
            _selectedOptionModel = selectedOptionModel;
            _upgradable = upgradable;
            _useResourceService = useResourceService;

            _eventAggregator.Subscribe(this);
        }

        public void Dispose()
        {
            _eventAggregator.Unsubscribe(this);
        }

        public void Handle(UpgradeOptionModelUpdateEvent @event)
        {
            if (@event.UpdatedModel != _model) return;
            if (_model.HasOption)
            {
                _view.ShowOption();

                _view.UpdateTurretCost(_model.Cost.ToString());
                _view.UpdateTurretName(_model.Name);
            }
            else
            {
                _view.HideOption();
            }
        }

        public void OnCheckOption()
        {
            _selectedOptionModel.SelectOption(_model);
        }

        public void OnSelectOption()
        {
            //this look ridiculously fishy...
            if (_useResourceService.TryUseResource(_selectedOptionModel.SelectedUpgradeOptionModel.TurretUpgradeEntry.TurretRepository.Cost))
            {
                _upgradable.UpgradeFrom(_selectedOptionModel.SelectedUpgradeOptionModel.TurretUpgradeEntry.TurretProvider.GetTurretPrefab());
            }
        }
    }
}