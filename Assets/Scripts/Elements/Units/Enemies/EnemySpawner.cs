using Common.Class;
using Common.Event;
using EventManagement;
using UnityEngine;
using UnityUtils.LocationProviders;

namespace Elements.Units.Enemies
{
    public class EnemySpawner : MonoBehaviour, IHandle<WaveStartEvent>
    {
        private IEventAggregator _eventAggregator;
        private int _spawnedEnemiesCount;
        private bool _startedAttack;
        private float _timer;

        [SerializeField] private EnemySpawnPointData enemySpawnPointData;
        [SerializeField] private LocationProvider locationProvider;
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
            if (!_startedAttack || _spawnedEnemiesCount >= enemySpawnPointData.TotalNumberOfEnemies)
            {
                return;
            }

            _timer += Time.deltaTime;
            if (_timer >= enemySpawnPointData.SpawnInterval)
            {
                _timer = 0f;

                var newEnemy = Instantiate(
                    enemySpawnPointData.EnemiesPrefabs[Random.Range(0, enemySpawnPointData.EnemiesPrefabs.Length)],
                    locationProvider.ProvideLocation(),
                    Quaternion.identity
                );

                newEnemy.transform.parent = transform;
                newEnemy.transform.Rotate(new Vector2(0f, Random.Range(0f, 360f)));
                
                _spawnedEnemiesCount++;
            }
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}