using Common;
using Common.Events;
using EventManagement;
using UnityEngine;

namespace Units.Enemies
{
    public class EnemySpawner : MonoBehaviour, IHandle<AttackBegins>
    {
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

        private void Awake()
        {
            EventAggregatorHolder.Instance.Subscribe(this);
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
                ObjectSpawner.Spawn(
                    enemySpawnPointData.EnemiesPrefabs[Random.Range(0, enemySpawnPointData.EnemiesPrefabs.Length)],
                    radius,
                    spawnPoint.position
                );
                _spawnedEnemiesCount++;

                if (_spawnedEnemiesCount >= enemySpawnPointData.TotalNumberOfEnemies) // only publish this once
                {
                    EventAggregatorHolder.Instance.Publish(new FinishedSpawningEvent());
                }
            }
        }

        private void OnDestroy()
        {
            EventAggregatorHolder.Instance.Unsubscribe(this);
        }
    }
}