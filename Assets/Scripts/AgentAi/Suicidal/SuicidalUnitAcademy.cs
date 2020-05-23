using System.Diagnostics;
using System.Linq;
using AgentAi.Manager;
using Common.Class;
using Common.Constant;
using Common.Event;
using Elements.Units.Players;
using EventManagement;
using MLAgents;
using TrainingSpecific.Events;
using UnityEngine;

namespace AgentAi.Suicidal
{
    public class SuicidalUnitAcademy : Academy, IHandle<AgentDoneEvent>, IHandle<AgentSpawnedEvent>,
                                       IHandle<WaveEndEvent>
    {
        private int _agentCount;
        private int _doneAgentCount;
        private IEventAggregator _eventAggregator;
        [Range(0, 5)] [SerializeField] private int initialLevel;
        [SerializeField] private ObjectsOfInterestTracker objectsOfInterestTracker;

        public void Handle(AgentDoneEvent @event)
        {
#if TRAINING
            //if all agent is done, loop for a new training stage
            _doneAgentCount++;

            if (_doneAgentCount >= _agentCount) ClearField();
#endif
        }

        public void Handle(AgentSpawnedEvent @event)
        {
#if TRAINING
            _agentCount++;
#endif
        }

        public void Handle(WaveEndEvent @event)
        {
            ClearField();
        }

        private void OnEnable()
        {
            if (Application.isEditor) FloatProperties.SetProperty(EnvironmentParametersKey.Level, initialLevel);

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

            _eventAggregator.Publish(
                new CurrentTrainingLevelEvent(
                    (int) FloatProperties.GetPropertyWithDefault(EnvironmentParametersKey.Level, 0f)
                )
            );
        }

        private void ClearField()
        {
            _agentCount = 0;
            _doneAgentCount = 0;
            ClearFieldForGameplay();
            ClearFieldForTraining();
        }

        [Conditional(GameConfig.TrainingMode)]
        private void ClearFieldForTraining()
        {
            foreach (var objectOfInterest in objectsOfInterestTracker.DynamicObjectOfInterests)
                _eventAggregator.Publish(new ForceResetEvent(objectOfInterest));
        }

        [Conditional(GameConfig.GameplayMode)]
        private void ClearFieldForGameplay()
        {
            foreach (var objectOfInterest in objectsOfInterestTracker.DynamicObjectOfInterests.Where(
                d => d.GetType() != typeof(Player)
            ))
                _eventAggregator.Publish(new ForceResetEvent(objectOfInterest));
        }

        private static class EnvironmentParametersKey
        {
            public const string Level = "level";
        }
    }
}