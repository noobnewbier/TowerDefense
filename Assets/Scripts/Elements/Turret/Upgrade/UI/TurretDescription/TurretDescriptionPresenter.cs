using System;
using EventManagement;

namespace Elements.Turret.Upgrade.UI.TurretDescription
{
    public interface ITurretDescriptionPresenter : IDisposable, IHandle<TurretDescriptionTextUpdateEvent>
    {
    }

    public class TurretDescriptionPresenter : ITurretDescriptionPresenter
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ITurretDescriptionView _view;
        private readonly ITurretDescriptionModel _model;

        public TurretDescriptionPresenter(IEventAggregator eventAggregator, ITurretDescriptionView view, ITurretDescriptionModel model)
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

        public void Handle(TurretDescriptionTextUpdateEvent @event)
        {
            if (@event.UpdatedDescription != _model) return;
            
            _view.UpdateText(_model.Description);
        }
    }
}