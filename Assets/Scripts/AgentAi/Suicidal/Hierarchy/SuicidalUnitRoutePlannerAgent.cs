using System;
using System.Diagnostics.CodeAnalysis;
using AgentAi.Manager;
using AgentAi.Manager.TargetPicker;
using Common.Class;
using Common.Enum;
using Common.Event;
using Common.Interface;
using Elements.Units.Enemies;
using Elements.Units.UnitCommon;
using EventManagement;
using MLAgents;
using Movement.InputSource;
using TrainingSpecific.Events;
using UnityEngine;
using UnityEngine.AI;

namespace AgentAi.Suicidal.Hierarchy
{
    public class SuicidalUnitRoutePlannerAgent : Agent, ITargetPicker, IHandle<EnemyDeadEvent>, ICanObserveEnvironment
    {
        private IEventAggregator _eventAggregator;
        private IObserveEnvironmentService _observeEnvironmentService;
        private float _previousClosestDistance;
        private ITargetPicker _targetPicker;

        [SerializeField] private SuicidalUnitRoutePlannerConfig config;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private ObservationServiceProvider observationServiceProvider;
        [SerializeField] private SuicidalEnemy unit;


        public Texture2D GetObservation()
        {
            return _observeEnvironmentService.CreateObservationAsTexture(unit, null);
        }

        public void Handle(EnemyDeadEvent @event)
        {
            if (@event.Enemy != unit) return;

            RewardIsDead(@event.DeathCause);
        }

        //cannot think of an elegant solution for heuristic controller... but this do the trick
        public override float[] Heuristic()
        {
            return new float[]
            {
            };
        }

        public override void InitializeAgent()
        {
            base.InitializeAgent();
            _eventAggregator = EventAggregatorHolder.Instance;
            _targetPicker = PickPlayerTargetPicker.Instance;
            _previousClosestDistance = GetCurrentDistanceFromTarget();
            _observeEnvironmentService = observationServiceProvider.ProvideService();

            _eventAggregator.Subscribe(this);
            _eventAggregator.Publish(new AgentSpawnedEvent());
        }

        public override void AgentOnDone()
        {
            base.AgentOnDone();
            _eventAggregator.Publish(new AgentDoneEvent());
            _eventAggregator.Unsubscribe(this);
        }

        public override void AgentAction(float[] vectorAction)
        {
            var newTargetPosition = InputToTargetPosition(vectorAction[0], vectorAction[1]);

            PunishRoaming();
            EncourageApproachingTarget();
        }

        [SuppressMessage("ReSharper", "RedundantCaseLabel")]
        private void RewardIsDead(EffectSource deathCause)
        {
            switch (deathCause)
            {
                case EffectSource.Player:
                    AddReward(config.KilledPunishment);
                    break;
                case EffectSource.System:
                    break;
                case EffectSource.SelfDestruction:
                    AddReward(config.SelfDestructionReward);
                    break;
                case EffectSource.Ai:
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Done();
        }

        //Don't walk around forever pls
        private void PunishRoaming()
        {
            AddReward(config.RoamingPunishment);
        }

        private void EncourageApproachingTarget()
        {
            var distance = GetCurrentDistanceFromTarget();

            if (distance < _previousClosestDistance)
            {
                var distanceDifference = _previousClosestDistance - distance;
                var maximumAchievement = _unitDataRepository.MaxForwardSpeed * Time.fixedDeltaTime;
                var rewardPercentage = Mathf.Clamp01(distanceDifference / maximumAchievement);

                AddReward(config.MaxApproachReward * rewardPercentage);
                _previousClosestDistance = distance;
            }
        }

        private float GetCurrentDistanceFromTarget()
        {
            // some default distance to a avoid bumping the reward to infinity when we can't find a path
            const float defaultDistance = 10f;
            var path = new NavMeshPath();
            if (!navMeshAgent.isOnNavMesh || !navMeshAgent.CalculatePath(
                    _targetPicker.RequestTarget().DynamicObjectTransform.position,
                    path
                )) return defaultDistance;

            if (path.status != NavMeshPathStatus.PathComplete) return defaultDistance;

            var distance = 0f;
            for (var i = 0; i < path.corners.Length - 1; i++)
                distance += Vector3.Distance(path.corners[i], path.corners[i + 1]);

            return distance;
        }

        public IDynamicObjectOfInterest RequestTarget()
        {
            throw new NotImplementedException();
        }

        private Vector2 InputToTargetPosition(float x, float y)
        {
            var xCord = Mathf.RoundToInt(Mathf.Clamp(x, -1, 1) * _observeEnvironmentService.Config.MapDimension / 2f);
            var yCord = Mathf.RoundToInt(Mathf.Clamp(y, -1, 1) * _observeEnvironmentService.Config.MapDimension / 2f);

            return new Vector2(xCord, yCord) * _observeEnvironmentService.Config.MapDimension / 2f;
        }
    }
}