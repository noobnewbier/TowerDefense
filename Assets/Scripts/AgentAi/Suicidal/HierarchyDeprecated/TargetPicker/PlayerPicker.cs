using AgentAi.Suicidal.HierarchyDeprecated.Event;
using EventManagement;
using Experimental;
using ScriptableService;
using UnityEngine;

namespace AgentAi.Suicidal.HierarchyDeprecated.TargetPicker
{
    public class PlayerPicker : MonoBehaviour, ITargetPicker
    {
        private IEventAggregator _localEventAggregator;
        [SerializeField] private LocalEventAggregatorProvider localEventAggregatorProvider;
        [SerializeField] private PlayerInstanceTracker playerInstanceTracker;

        public void Handle(FinishPathingEvent @event)
        {
            //do nothing
        }

        public void Handle(RequestNewTargetEvent @event)
        {
            _localEventAggregator.Publish(new NewTargetIssuedEvent(playerInstanceTracker.Player));
        }

        private void OnEnable()
        {
            _localEventAggregator = localEventAggregatorProvider.ProvideEventAggregator();

            _localEventAggregator.Subscribe(this);
        }

        private void OnDisable()
        {
            _localEventAggregator.Unsubscribe(this);
        }
    }
}