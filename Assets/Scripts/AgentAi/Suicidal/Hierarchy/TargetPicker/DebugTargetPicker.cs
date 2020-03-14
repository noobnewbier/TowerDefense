using AgentAi.Suicidal.Hierarchy.Event;
using EventManagement;
using Experimental;
using UnityEngine;

namespace AgentAi.Suicidal.Hierarchy.TargetPicker
{
    public class DebugTargetPicker : MonoBehaviour, ITargetPicker
    {
        private IEventAggregator _eventAggregator;
        [SerializeField] private LocalEventAggregatorProvider localEventAggregatorProvider;
        [SerializeField] private TargetMarker targetMarker;

        public void Handle(FinishPathingEvent @event)
        {
            //do nothing
        }

        public void Handle(RequestNewTargetEvent @event)
        {
            _eventAggregator.Publish(new NewTargetIssuedEvent(targetMarker));
        }


        private void OnEnable()
        {
            _eventAggregator = localEventAggregatorProvider.ProvideEventAggregator();

            _eventAggregator.Subscribe(this);
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}