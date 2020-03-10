using Common.Class;
using Common.Event;
using Common.Interface;
using EventManagement;
using UnityEngine;

namespace AgentAi.Manager.TargetPicker
{
    public class PickPlayerTargetPicker : MonoBehaviour, ITargetPicker, IHandle<PlayerSpawnedEvent>,
                                          IHandle<PlayerDeadEvent>
    {
        private IEventAggregator _eventAggregator;
        private IDynamicObjectOfInterest _target;
        public static PickPlayerTargetPicker Instance { get; private set; }

        public void Handle(PlayerDeadEvent @event)
        {
            _target = null;
        }

        public void Handle(PlayerSpawnedEvent @event)
        {
            _target = @event.Player;
        }

        public IDynamicObjectOfInterest RequestTarget()
        {
            return _target;
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
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