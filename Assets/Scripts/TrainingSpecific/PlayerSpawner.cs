using Common.Class;
using Elements.Units.Players;
using EventManagement;
using ScriptableService;
using TrainingSpecific.Events;
using UnityEngine;
using UnityUtils.LocationProviders;
using UnityUtils.ScaleProviders;

namespace TrainingSpecific
{
    public class PlayerSpawner : MonoBehaviour, IHandle<SpawnPlayerEvent>
    {
        private IEventAggregator _eventAggregator;

        [SerializeField] private Player playerPrefab;

        // need to be length of threshold in curriculum + 1
        [SerializeField] private ScaleProvider scaleProvider;
        [SerializeField] private LocationProvider spawnPoint;
        [SerializeField] private SpawnPointValidator spawnPointValidator;

        public void Handle(SpawnPlayerEvent @event)
        {
            var playerInstance = Instantiate(playerPrefab.gameObject);
            var playerComponent = playerInstance.GetComponent<Player>();
            playerInstance.transform.localScale = scaleProvider.ProvideScale();

            do
            {
                playerInstance.transform.position = spawnPoint.ProvideLocation();
            } while (!spawnPointValidator.IsSpawnPointValid(
                playerInstance.transform.position + Vector3.up * 0.75f,
                playerComponent.Bounds.size / 2f,
                Quaternion.identity,
                playerInstance
            ));
        }

        private void OnEnable()
        {
            _eventAggregator = EventAggregatorHolder.Instance;

            _eventAggregator.Subscribe(this);
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}