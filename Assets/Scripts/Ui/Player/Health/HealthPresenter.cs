using System;
using Elements.Units.UnitCommon;
using EventManagement;

namespace Ui.Player.Health
{
    public interface IHealthPresenter : IHandle<UnitHealthChangedEvent>, IDisposable
    {
    }

    public class HealthPresenter : IHealthPresenter
    {
        private readonly IUnitDataRepository _unitDataRepository;
        private readonly IHealthView _view;
        private readonly IEventAggregator _eventAggregator;
        private readonly Unit _presentedUnit;

        public HealthPresenter(IUnitDataRepository unitDataRepository, Unit presentedUnit, IHealthView view,
            IEventAggregator eventAggregator
        )
        {
            _unitDataRepository = unitDataRepository;
            _presentedUnit = presentedUnit;
            _view = view;
            _eventAggregator = eventAggregator;

            _eventAggregator.Subscribe(this);
        }


        public void Handle(UnitHealthChangedEvent @event)
        {
            if(@event.UnitChanged != _presentedUnit) return;

            _view.OnHealthUpdate(_unitDataRepository.Health / _unitDataRepository.MaxHealth);
        }

        public void Dispose()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}