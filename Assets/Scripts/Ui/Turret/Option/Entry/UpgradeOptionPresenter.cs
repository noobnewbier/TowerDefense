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
        private readonly IUpgradeOptionModel _model;
        private readonly IPlaceTurretService _placeTurretService;
        private readonly ISelectedOptionModel _selectedOptionModel;
        private readonly IUpgradable _upgradable;
        private readonly IUseResourceService _useResourceService;
        private readonly IUpgradeOptionView _view;

        public UpgradeOptionPresenter(
            IEventAggregator eventAggregator,
            IUpgradeOptionView view,
            IUpgradeOptionModel model,
            ISelectedOptionModel selectedOptionModel,
            IUpgradable upgradable,
            IUseResourceService useResourceService,
            IPlaceTurretService placeTurretService)
        {
            _eventAggregator = eventAggregator;
            _view = view;
            _model = model;
            _selectedOptionModel = selectedOptionModel;
            _upgradable = upgradable;
            _useResourceService = useResourceService;
            _placeTurretService = placeTurretService;

            _eventAggregator.Subscribe(this);
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

        public void Dispose()
        {
            _eventAggregator.Unsubscribe(this);
        }

        public void OnCheckOption()
        {
            _selectedOptionModel.SelectOption(_model);
        }

        public void OnSelectOption()
        {
            //this look ridiculously fishy...
            if (_useResourceService.TryUseResource(
                _selectedOptionModel.SelectedUpgradeOptionModel.TurretUpgradeEntry.TurretRepository.Cost
            ))
            {
                _upgradable.Destruct();
                var newTurretProvider =
                    _selectedOptionModel.SelectedUpgradeOptionModel.TurretUpgradeEntry.TurretProvider;
                _placeTurretService.PlaceTurret(
                    newTurretProvider,
                    _upgradable.CurrentTransform
                );
            }
        }
    }
}