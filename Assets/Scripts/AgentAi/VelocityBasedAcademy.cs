using System.Diagnostics;
using Common.Class;
using Common.Constant;
using EventManagement;
using MLAgents;
using TrainingSpecific;

namespace AgentAi
{
    public class VelocityBasedAcademy : Academy, IHandle<AgentDoneEvent>, IHandle<AgentSpawnedEvent>
    {
        private IEventAggregator _eventAggregator;
        private int _agentCount;
        private int _doneAgentCount;

        private void OnEnable()
        {
            _eventAggregator = EventAggregatorHolder.Instance;

            _eventAggregator.Subscribe(this);
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }

        public override void AcademyReset()
        {
            _agentCount = 0;
            _doneAgentCount = 0;
            _eventAggregator.Publish(new ForceResetEvent());
        }

        public void Handle(AgentDoneEvent @event)
        {
#if TRAINING
            _doneAgentCount++;
            if (_doneAgentCount >= _agentCount)
            {
                _eventAggregator.Publish(new ForceResetEvent());
            }
#endif
        }

        public void Handle(AgentSpawnedEvent @event)
        {
#if TRAINING
            _agentCount++;
#endif
        }
    }
}