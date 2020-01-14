using System;
using EventManagement;
using Ui.TurretUpgrade.Option;
using Ui.TurretUpgrade.Option.ListView;

namespace Ui.TurretUpgrade.TurretDescription
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