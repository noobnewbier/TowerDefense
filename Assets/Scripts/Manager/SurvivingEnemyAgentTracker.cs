using System;
using System.Collections.Generic;
using System.Linq;
using Common.Class;
using Common.Event;
using Elements.Units.Enemies;
using EventManagement;
using UnityEngine;

namespace Manager
{
    public class SurvivingEnemyAgentTracker : MonoBehaviour, IHandle<EnemyDeadEvent>, IHandle<EnemySpawnedEvent>, IHandle<WaveStartEvent>
    {
        private IEventAggregator _eventAggregator;
        private int _totalEnemiesCount;
        private int _spawnedEnemiesCount;

        public IList<Enemy> EnemiesInField { get; private set; }

        private void OnEnable()
        {
            EnemiesInField = new List<Enemy>();

            _eventAggregator = EventAggregatorHolder.Instance;
            _eventAggregator.Subscribe(this);
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }

        public void Handle(EnemyDeadEvent @event)
        {
            EnemiesInField.Remove(@event.Enemy);

            if (!EnemiesInField.Any() && _spawnedEnemiesCount == _totalEnemiesCount)
            {
                _eventAggregator.Publish(new AllEnemyAgentsDeadEvent());
            }
        }

        public void Handle(EnemySpawnedEvent @event)
        {
            _spawnedEnemiesCount++;
            EnemiesInField.Add(@event.Enemy);
        }

        public void Handle(WaveStartEvent @event)
        {
            if (EnemiesInField.Any())
            {
                throw new InvalidOperationException(
                    "All enemies should be correctly tracked, and no enemies should exist when the wave starts, and hence if there is one, there is an error"
                );
            }

            _totalEnemiesCount = FindObjectsOfType<EnemySpawner>().Sum(spawner => spawner.TotalEnemyCount);
            _spawnedEnemiesCount = 0;
        }
    }
}