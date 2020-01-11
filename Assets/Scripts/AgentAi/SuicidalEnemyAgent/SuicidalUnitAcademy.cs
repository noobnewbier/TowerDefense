using AgentAi.Manager;
using Common.Class;
using EventManagement;
using MLAgents;
using TrainingSpecific.Events;
using UnityEngine;

namespace AgentAi.SuicidalEnemyAgent
{
    public class SuicidalEnemyAcademy : Academy, IHandle<AgentDoneEvent>, IHandle<AgentSpawnedEvent>
    {
        private int _agentCount;
        private int _doneAgentCount;
        private IEventAggregator _eventAggregator;
        [SerializeField] private ObjectsOfInterestTracker objectsOfInterestTracker;


        //bug here!
        public void Handle(AgentDoneEvent @event)
        {
#if TRAINING
            _doneAgentCount++;

            if (_doneAgentCount >= _agentCount)
            {
                ClearField();
            }
#endif
        }

        public void Handle(AgentSpawnedEvent @event)
        {
#if TRAINING
            _agentCount++;
#endif
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

        public override void AcademyReset()
        {
            ClearField();

            _eventAggregator.Publish(new CurrentTrainingLevelEvent((int) resetParameters[EnvironmentParametersKey.Level]));
        }

        private void ClearField()
        {
            _agentCount = 0;
            _doneAgentCount = 0;

            foreach (var objectOfInterest in objectsOfInterestTracker.DynamicObjectOfInterests) _eventAggregator.Publish(new ForceResetEvent(objectOfInterest));
        }

        private static class EnvironmentParametersKey
        {
            public const string Level = "level";
        }
    }
}