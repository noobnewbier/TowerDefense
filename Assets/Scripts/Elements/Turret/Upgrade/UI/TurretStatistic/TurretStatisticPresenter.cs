using System;
using Common.Constant;
using Elements.Turret.Upgrade.UI.Option;
using Elements.Turret.Upgrade.UI.Option.ListView;
using EventManagement;
using TMPro;

namespace Elements.Turret.Upgrade.UI.TurretStatistic
{
    public interface IStatisticPresenter : IDisposable
    {
    }

    public class TurretStatisticPresenter : IStatisticPresenter, IHandle<SelectUpgradeOptionEvent>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISelectedOptionRepository _model;
        private readonly IStatisticView _view;

        public TurretStatisticPresenter(IStatisticView view, ISelectedOptionRepository model, IEventAggregator eventAggregator)
        {
            _view = view;
            _model = model;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        public void Handle(SelectUpgradeOptionEvent @event)
        {
            _view.UpdateDamage(_model.SelectedUpgradeOptionModel.DamageForUiDisplay / UiConfig.MaxDamage);
            _view.UpdateRange(_model.SelectedUpgradeOptionModel.DetectionRange / UiConfig.MaxRange);
            _view.UpdateShootFrequency(_model.SelectedUpgradeOptionModel.ShootFrequency / UiConfig.MaxShootFrequency);
        }

        public void Dispose()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}