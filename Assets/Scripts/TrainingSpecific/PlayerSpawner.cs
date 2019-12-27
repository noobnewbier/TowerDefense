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
    public class PlayerSpawner : MonoBehaviour, IHandle<SessionBeginEvent>
    {
        private IEventAggregator _eventAggregator;

        [SerializeField] private Player playerPrefab;

        // need to be length of threshold in curriculum + 1
        [SerializeField] private ScaleProvider scaleProvider;
        [SerializeField] private LocationProvider spawnPoint;

        public void Handle(SessionBeginEvent @event)
        {
            var playerInstance = Instantiate(playerPrefab.gameObject);
            playerInstance.transform.localScale = scaleProvider.ProvideScale();

            var playerBounds = playerInstance.GetComponent<Player>().Bounds;
            do
            {
                playerInstance.transform.position = spawnPoint.ProvideLocation();
            } while (!SpawnPointValidator.IsSpawnPointValid(playerBounds.center, playerBounds.extents, playerInstance.transform.rotation));
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