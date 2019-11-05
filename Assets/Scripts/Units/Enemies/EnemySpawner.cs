using Common;
using Common.Events;
using EventManagement;
using UnityEngine;

namespace Units.Enemies
{
    public class EnemySpawner : MonoBehaviour, IHandle<AttackBegins>
    {
        private IEventAggregator _eventAggregator;
        private int _spawnedEnemiesCount;
        private bool _startedAttack;
        private float _timer;
        
        [SerializeField] private EnemySpawnPointData enemySpawnPointData;
        //Spawn within radius
        [SerializeField] private float radius;
        [SerializeField] private Transform spawnPoint;

        public void Handle(AttackBegins @event)
        {
            _startedAttack = true;
        }

        private void OnEnable()
        {
            _eventAggregator = EventAggregatorHolder.Instance;
            _eventAggregator.Subscribe(this);
        }

        private void Update()
        {
            if (!_startedAttack || _spawnedEnemiesCount >= enemySpawnPointData.TotalNumberOfEnemies)
            {
                return;
            }

            _timer += Time.deltaTime;
            if (_timer >= enemySpawnPointData.SpawnInterval)
            {
                _timer = 0f;
                ObjectSpawner.SpawnInCircle(
                    enemySpawnPointData.EnemiesPrefabs[Random.Range(0, enemySpawnPointData.EnemiesPrefabs.Length)],
                    radius,
                    spawnPoint.position
                );
                _spawnedEnemiesCount++;

                if (_spawnedEnemiesCount >= enemySpawnPointData.TotalNumberOfEnemies) // only publish this once
                {
                    _eventAggregator.Publish(new FinishedSpawningEvent());
                }
            }
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}