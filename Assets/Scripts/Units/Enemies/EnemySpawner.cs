using Common;
using Common.Events;
using EventManagement;
using UnityEngine;

namespace Units.Enemies
{
    public class EnemySpawner : MonoBehaviour, IHandle<AttackBegins>
    {
        [SerializeField] private Transform spawnPoint;

        //Spawn within radius
        [SerializeField] private float radius;
        [SerializeField] private EnemySpawnPointData enemySpawnPointData;

        private bool _startedAttack;
        private float _timer;
        private int _spawnedEnemiesCount;

        private void Awake()
        {
            EventAggregatorHolder.Instance.Subscribe(this);
        }

        public void Handle(AttackBegins @event)
        {
            _startedAttack = true;
        }

        private void Update()
        {
            if (!_startedAttack || _spawnedEnemiesCount >= enemySpawnPointData.TotalNumberOfEnemies) return;

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
            }
        }

        private void OnDestroy()
        {
            EventAggregatorHolder.Instance.Unsubscribe(this);
        }
    }
}