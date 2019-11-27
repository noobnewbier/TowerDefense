using Common.Class;
using Common.Event;
using EventManagement;
using UnityEngine;

namespace Elements.Units.Enemies
{
    public class EnemySpawner : MonoBehaviour, IHandle<WaveStartEvent>
    {
        private IEventAggregator _eventAggregator;
        private int _spawnedEnemiesCount;
        private bool _startedAttack;
        private float _timer;

        [SerializeField] private EnemySpawnPointData enemySpawnPointData;

        //Spawn within radius
        [SerializeField] private float radius;
        [SerializeField] private Transform spawnPoint;
        public int TotalEnemyCount => enemySpawnPointData.TotalNumberOfEnemies;

        public void Handle(WaveStartEvent @event)
        {
            _startedAttack = true;

            _spawnedEnemiesCount = 0; //reset this crap so it will do it again
        }

        private void OnEnable()
        {
            _eventAggregator = EventAggregatorHolder.Instance;
            _eventAggregator.Subscribe(this);
        }

        private void Update()
        {
            if (!_startedAttack || _spawnedEnemiesCount >= enemySpawnPointData.TotalNumberOfEnemies) return;

            _timer += Time.deltaTime;
            if (_timer >= enemySpawnPointData.SpawnInterval)
            {
                _timer = 0f;
                ObjectSpawner.SpawnInCircle(
                    enemySpawnPointData.EnemiesPrefabs[Random.Range(0, enemySpawnPointData.EnemiesPrefabs.Length)],
                    radius,
                    spawnPoint.position
                ).transform.parent = transform;


                _spawnedEnemiesCount++;
            }
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}