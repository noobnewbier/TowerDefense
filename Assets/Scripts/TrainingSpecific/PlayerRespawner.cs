using Common;
using EventManagement;
using UnityEngine;

namespace TrainingSpecific
{
    public class PlayerRespawner : MonoBehaviour, IHandle<SpawnPlayerEvent>
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject playerPrefab;

        private IEventAggregator _eventAggregator;

        private void OnEnable()
        {
            _eventAggregator = EventAggregatorHolder.Instance;
            
            _eventAggregator.Subscribe(this);
        }

        public void Handle(SpawnPlayerEvent @event)
        {
            Instantiate(playerPrefab).transform.position = spawnPoint.transform.position;
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}