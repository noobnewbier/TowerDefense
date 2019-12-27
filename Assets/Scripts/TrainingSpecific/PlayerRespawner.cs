using Common.Class;
using Common.Event;
using EventManagement;
using TrainingSpecific.Events;
using UnityEngine;
using UnityUtils.LocationProviders;

namespace TrainingSpecific
{
    public class PlayerRespawner : MonoBehaviour, IHandle<SessionBeginEvent>
    {
        private LevelTracker _levelTracker;
        private IEventAggregator _eventAggregator;

        [SerializeField] private GameObject playerPrefab;

        // need to be length of threshold in curriculum + 1
        [SerializeField] private Vector3[] scales;
        [SerializeField] private LocationProvider spawnPoint;


        public void Handle(SessionBeginEvent @event)
        {
            var playerInstance = Instantiate(playerPrefab);
            playerInstance.transform.localScale = Vector3.Scale(playerInstance.transform.localScale, scales[_levelTracker.CurrentLevel]);

            playerInstance.transform.position = spawnPoint.ProvideLocation();
        }

        private void OnEnable()
        {
            _eventAggregator = EventAggregatorHolder.Instance;
            _levelTracker = LevelTracker.Instance;
            
            _eventAggregator.Subscribe(this);
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}