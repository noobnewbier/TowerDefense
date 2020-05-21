using Elements.Units.Enemies;
using Elements.Units.Enemies.Provider;
using EventManagement;
using EventManagement.Providers;
using TrainingSpecific.Events;
using UnityEngine;

namespace TrainingSpecific.Providers
{
    public class RefreshOnTurnStartEnemySpawnPointDataProvider : EnemySpawnPointDataProvider, IHandle<TurnStartEvent>
    {
        private EnemySpawnPointData _currentData;
        private IEventAggregator _eventAggregator;
        [SerializeField] private EventAggregatorProvider eventAggregatorProvider;
        [SerializeField] private EnemySpawnPointDataProvider spawnPointDataProvider;

        public void Handle(TurnStartEvent @event)
        {
            _currentData = spawnPointDataProvider.ProvideData();
        }

        public override EnemySpawnPointData ProvideData()
        {
            return _currentData;
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