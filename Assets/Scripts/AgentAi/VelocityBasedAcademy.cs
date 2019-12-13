using System.Collections.Generic;
using Common.Class;
using Common.Interface;
using Elements.Units.Players;
using EventManagement;
using Manager;
using MLAgents;
using TrainingSpecific;
using UnityEngine;

namespace AgentAi
{
    public class VelocityBasedAcademy : Academy, IHandle<AgentDoneEvent>, IHandle<AgentSpawnedEvent>
    {
        public static class EnvironmentParametersKey
        {
            public const string Level = "level";
        }
        
        [SerializeField] private SurvivingEnemyAgentTracker survivingEnemyAgentTracker;
        [SerializeField] private SurvivingTurretTracker survivingTurretTracker;

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
            ClearField();
            
            _eventAggregator.Publish(new ResetAcademyEvent(this));
        }

        public void Handle(AgentSpawnedEvent @event)
        {
#if TRAINING
            _agentCount++;
#endif
        }

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
    }
}