using EventManagement;
using Experimental;
using TrainingSpecific.Events;
using UnityEngine;
using UnityUtils.FloatProvider;

namespace TrainingSpecific.Providers
{
    public class RefreshOnTurnStartFloatProvider : FloatProvider, IHandle<TurnStartEvent>
    {
        private float _currentFloat;
        private IEventAggregator _eventAggregator;
        [SerializeField] private EventAggregatorProvider eventAggregatorProvider;
        [SerializeField] private FloatProvider randomFloatProvider;

        public void Handle(TurnStartEvent @event)
        {
            _currentFloat = randomFloatProvider.ProvideFloat();
        }

        public override float ProvideFloat()
        {
            return _currentFloat;
        }

        private void OnEnable()
        {
            _eventAggregator = eventAggregatorProvider.ProvideEventAggregator();
            _eventAggregator.Subscribe(this);
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}