using Common.Class;
using Common.Event;
using Elements.Units.Enemies.Provider;
using EventManagement;
using ScriptableService;
using UnityEngine;
using UnityUtils.FloatProvider;
using UnityUtils.LocationProviders;

namespace Elements.Units.Enemies
{
    public class EnemySpawner : MonoBehaviour, IHandle<WaveStartEvent>, IHandle<WaveEndEvent>
    {
        private IEventAggregator _eventAggregator;
        private int _spawnedEnemiesCount;
        private bool _startedAttack;
        private float _timer;

        [SerializeField] private EnemySpawnPointDataProvider enemySpawnPointDataProvider;
        [SerializeField] private LocationProvider locationProvider;
        [SerializeField] private FloatProvider spawnedEnemyOrientationProvider;
        [SerializeField] private SpawnPointValidator spawnPointValidator;


        public int TotalEnemyCount => enemySpawnPointDataProvider.ProvideData().TotalNumberOfEnemies;

        public void Handle(WaveEndEvent @event)
        {
            _startedAttack = false;
            _spawnedEnemiesCount = 0; //reset this crap so it will do it again
        }

        public void Handle(WaveStartEvent @event)
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
            if (!_startedAttack ||
                _spawnedEnemiesCount >= enemySpawnPointDataProvider.ProvideData().TotalNumberOfEnemies) return;

            _timer += Time.deltaTime;
            if (_timer >= enemySpawnPointDataProvider.ProvideData().SpawnInterval)
            {
                _timer = 0f;

                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            var newEnemyGameObject = Instantiate(
                enemySpawnPointDataProvider.ProvideData().Enemies[Random.Range(
                    0,
                    enemySpawnPointDataProvider.ProvideData().Enemies.Length
                )].gameObject,
                locationProvider.ProvideLocation(),
                Quaternion.identity
            );
            newEnemyGameObject.transform.parent = transform;
            newEnemyGameObject.transform.Rotate(new Vector2(0f, spawnedEnemyOrientationProvider.ProvideFloat()));

            while (!spawnPointValidator.IsSpawnPointValid(
                newEnemyGameObject.transform.position,
                newEnemyGameObject.transform.localScale / 2f,
                newEnemyGameObject.transform.rotation,
                newEnemyGameObject
            ))
                newEnemyGameObject.transform.position = locationProvider.ProvideLocation();


            _spawnedEnemiesCount++;
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}