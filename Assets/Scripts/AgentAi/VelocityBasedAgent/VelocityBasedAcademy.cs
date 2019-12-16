using System.Collections.Generic;
using Common.Class;
using Common.Interface;
using Elements.Units.Players;
using EventManagement;
using Manager;
using MLAgents;
using TrainingSpecific;
using UnityEngine;

namespace AgentAi.VelocityBasedAgent
{
    public class VelocityBasedAcademy : Academy, IHandle<AgentDoneEvent>, IHandle<AgentSpawnedEvent>
    {
        private int _agentCount;
        private int _doneAgentCount;

        private IEventAggregator _eventAggregator;

        [SerializeField] private SurvivingEnemyAgentTracker survivingEnemyAgentTracker;
        [SerializeField] private SurvivingTurretTracker survivingTurretTracker;

        public void Handle(AgentDoneEvent @event)
        {
#if TRAINING
            _doneAgentCount++;

            if (_doneAgentCount == _agentCount)
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

            _eventAggregator.Publish(new ResetAcademyEvent(this));
        }

        private void ClearField()
        {
            _agentCount = 0;
            _doneAgentCount = 0;

            var dynamicObjectsOnField = new List<IDynamicObjectOfInterest>();

            dynamicObjectsOnField.AddRange(survivingTurretTracker.TurretsInField);
            dynamicObjectsOnField.AddRange(survivingEnemyAgentTracker.EnemiesInField);
            var player = FindObjectOfType<Player>();
            if (player != null)
            {
                dynamicObjectsOnField.Add(player);
            }

            dynamicObjectsOnField.ForEach(d => _eventAggregator.Publish(new ForceResetEvent(d)));
        }

        public static class EnvironmentParametersKey
        {
            public const string Level = "level";
        }
    }
}