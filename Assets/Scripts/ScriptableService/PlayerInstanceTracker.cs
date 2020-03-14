using Common.Event;
using Elements.Units.Players;
using EventManagement;
using Experimental;
using UnityEngine;

namespace ScriptableService
{
    [CreateAssetMenu(menuName = "ScriptableService/PlayerInstanceTracker")]
    public class PlayerInstanceTracker : ScriptableObject, IHandle<PlayerSpawnedEvent>, IHandle<PlayerDeadEvent>
    {
        [SerializeField] private EventAggregatorProvider eventAggregatorProvider;
        public Player Player { get; private set; }

        private IEventAggregator _eventAggregator;

        public void Handle(PlayerDeadEvent @event)
        {
            Player = null;
        }

        public void Handle(PlayerSpawnedEvent @event)
        {
            Player = @event.Player;
        }

        private void OnEnable()
        {
            _eventAggregator = eventAggregatorProvider.ProvideEventAggregator();

            _eventAggregator.Subscribe(this);
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}