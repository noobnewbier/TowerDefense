using System;
using EventManagement;

namespace Elements.Turret.Upgrade.UI.TurretStatistic
{
    public interface IStatisticPresenter : IDisposable
    {
    }

    public class TurretStatisticPresenter : IStatisticPresenter, IHandle<TurretStatisticModelUpdatesEvent>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IStatisticModel _model;
        private readonly IStatisticView _view;

        public TurretStatisticPresenter(IStatisticView view, IStatisticModel model, IEventAggregator eventAggregator)
        {
            _view = view;
            _model = model;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        public void Handle(TurretStatisticModelUpdatesEvent @event)
        {
            if (@event.UpdatedModel != _model) return;

            _view.UpdateView(_model.Value / _model.MaxValue);
        }

        public void Dispose()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}