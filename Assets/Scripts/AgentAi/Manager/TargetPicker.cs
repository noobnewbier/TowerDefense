using Common.Class;
using Common.Event;
using Common.Interface;
using EventManagement;
using UnityEngine;

namespace AgentAi.Manager
{
    public interface ITargetPicker
    {
        IDynamicObjectOfInterest Target { get; }
    }
    
    public class TargetPicker : MonoBehaviour, ITargetPicker, IHandle<PlayerSpawnedEvent>
    {
        public static TargetPicker Instance { get; private set; }
        public IDynamicObjectOfInterest Target { get; private set; }
        private IEventAggregator _eventAggregator;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void Handle(PlayerSpawnedEvent @event)
        {
            Target = @event.Player;
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