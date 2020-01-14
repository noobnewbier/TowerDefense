using System;
using Elements.Turret.Upgrade.UI.Option;
using Elements.Turret.Upgrade.UI.Option.ListView;
using EventManagement;

namespace Elements.Turret.Upgrade.UI.TurretDescription
{
    public interface ITurretDescriptionPresenter : IDisposable, IHandle<SelectUpgradeOptionEvent>
    {
    }

    public class TurretDescriptionPresenter : ITurretDescriptionPresenter
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ITurretDescriptionView _view;
        private readonly ISelectedOptionModel _model;

        public TurretDescriptionPresenter(IEventAggregator eventAggregator, ITurretDescriptionView view, ISelectedOptionModel model)
        {
            _eventAggregator = eventAggregator;
            _view = view;
            _model = model;
            
            _eventAggregator.Subscribe(this);
        }

        public void Dispose()
        {
            _eventAggregator.Unsubscribe(this);
        }

        public void Handle(SelectUpgradeOptionEvent @event)
        {
            _view.UpdateText(_model.SelectedUpgradeOptionModel.Description);
        }
    }
}