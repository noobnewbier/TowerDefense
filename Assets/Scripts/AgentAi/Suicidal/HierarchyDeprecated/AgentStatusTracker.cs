using AgentAi.Suicidal.HierarchyDeprecated.Event;
using EventManagement;
using EventManagement.Providers;
using Experimental;
using TrainingSpecific.Events;
using UnityEngine;

namespace AgentAi.Suicidal.HierarchyDeprecated
{
    public class AgentStatusTracker : MonoBehaviour, IHandle<SubAgentSpawnedEvent>, IHandle<SubAgentDoneEvent>
    {
        private IEventAggregator _globalEventAggregator;
        private IEventAggregator _localEventAggregator;
        private int _unfinishedAgentCount;
        [SerializeField] private EventAggregatorProvider globalEventAggregatorProvider;
        [SerializeField] private LocalEventAggregatorProvider localEventAggregatorProvider;

        public void Handle(SubAgentDoneEvent @event)
        {
            _unfinishedAgentCount--;
            if (_unfinishedAgentCount == 0) _globalEventAggregator.Publish(new AgentDoneEvent());
        }

        public void Handle(SubAgentSpawnedEvent @event)
        {
            _unfinishedAgentCount++;
        }

        private void OnEnable()
        {
            _localEventAggregator = localEventAggregatorProvider.ProvideEventAggregator();
            _globalEventAggregator = globalEventAggregatorProvider.ProvideEventAggregator();

            _localEventAggregator.Subscribe(this);
        }

        private void OnDisable()
        {
            _localEventAggregator.Unsubscribe(this);
        }
    }
}