using Common.Class;
using Common.Event;
using EventManagement;
using ScriptableService;
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

                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            var newEnemyComponent = enemySpawnPointData.Enemies[Random.Range(0, enemySpawnPointData.Enemies.Length)];
            var newEnemyGameObject = Instantiate(
                newEnemyComponent.gameObject,
                locationProvider.ProvideLocation(),
                Quaternion.identity
            );

            newEnemyGameObject.transform.parent = transform;
            newEnemyGameObject.transform.Rotate(new Vector2(0f, Random.Range(0f, 360f)));

            while (!SpawnPointValidator.IsSpawnPointValid(
                newEnemyComponent.Bounds.center,
                newEnemyComponent.Bounds.extents,
                newEnemyGameObject.transform.rotation
            ))
            {
                newEnemyGameObject.transform.position = locationProvider.ProvideLocation();
            }

            _spawnedEnemiesCount++;
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}