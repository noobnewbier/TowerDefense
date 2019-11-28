using Common.Class;
using EventManagement;
using UnityEngine;

namespace TrainingSpecific
{
    public class PlayerRespawner : MonoBehaviour, IHandle<SpawnPlayerEvent>
    {
        private IEventAggregator _eventAggregator;
        private GameObject _playerInstance;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform spawnPoint;

        public void Handle(SpawnPlayerEvent @event)
        {
            if (_playerInstance != null)
            {
                Destroy(_playerInstance);
            }

            _playerInstance = Instantiate(playerPrefab);
            _playerInstance.transform.position = spawnPoint.transform.position;
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