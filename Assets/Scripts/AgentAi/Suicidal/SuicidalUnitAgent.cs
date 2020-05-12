using System;
using System.Diagnostics.CodeAnalysis;
using AgentAi.Manager;
using Common.Class;
using Common.Constant;
using Common.Enum;
using Common.Event;
using Elements.Units.Enemies;
using Elements.Units.UnitCommon;
using EventManagement;
using MLAgents;
using Movement.InputSource;
using ScriptableService;
using TrainingSpecific.Events;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityUtils;

namespace AgentAi.Suicidal
{
    //todo: consider refactoring, it feels like this is doing too much. Consider outsourcing reward calculation
    public class SuicidalUnitAgent : Agent, IHandle<EnemyDeadEvent>, IHandle<UnitHealthChangedEvent>,
                                     ICanObserveEnvironment, ICollisionStayDelegate
    {
        private IEventAggregator _eventAggregator;
        private IObserveEnvironmentService _observeEnvironmentService;
        private float _previousClosestDistance;
        private IUnitDataRepository _unitDataRepository;

        [SerializeField] private SuicidalUnitAgentConfig config;
        [SerializeField] private AiMovementInputService inputService;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private ObservationServiceProvider observationServiceProvider;
        [SerializeField] private PlayerInstanceTracker playerInstanceTracker;
        [SerializeField] private SuicidalEnemy unit;

        [FormerlySerializedAs("provider")] [SerializeField]
        private UnitProvider unitProvider;


        public Texture2D GetObservation()
        {
            return _observeEnvironmentService.CreateObservationAsTexture(
                unit,
                playerInstanceTracker.Player,
                this
            );
        }

        public void OnCollisionStayCalled(Collision other)
        {
            if (other.collider.CompareTag(ObjectTags.Wall))
                AddReward(config.ContactWithObstaclePunishment);
        }

        public void Handle(EnemyDeadEvent @event)
        {
            if (@event.Enemy != unit) return;

            RewardIsDead(@event.DeathCause);
        }

        public void Handle(UnitHealthChangedEvent @event)
        {
            if (@event.UnitChanged != unit || @event.EffectSource == EffectSource.System) return;

            AddReward(config.PerDamagePunishment * -@event.Amount);
        }

        //cannot think of an elegant solution for heuristic controller... but this do the trick
        public override float[] Heuristic()
        {
            return new float[]
            {
                PlayerInputToMachineInput(Input.GetAxis("Vertical")),
                PlayerInputToMachineInput(Input.GetAxis("Horizontal"))
            };
        }

        public override void InitializeAgent()
        {
            base.InitializeAgent();
            _eventAggregator = EventAggregatorHolder.Instance;
            _previousClosestDistance = GetCurrentDistanceFromTarget();
            _unitDataRepository = unitProvider.ProvideUnitDataRepository();
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
            var yAction = IsInValidInput(vectorAction[0]) ? 0 : (int) vectorAction[0];
            var xAction = IsInValidInput(vectorAction[1]) ? 0 : (int) vectorAction[1];

            if (!config.UseContinuousOutput)
            {
                yAction = MachineInputToAction(yAction);
                xAction = MachineInputToAction(xAction);
            }

            inputService.UpdateVertical(yAction);
            inputService.UpdateHorizontal(xAction);
            PunishRoaming();
            EncourageApproachingTarget();
            
            Debug.Log(GetCumulativeReward());
        }

        public override void CollectObservations()
        {
            base.CollectObservations();
            if (config.UseVectorRotation) AddRotationObservation();
        }

        private void AddRotationObservation()
        {
            AddVectorObs(unit.transform.rotation.eulerAngles.y / 360f);
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
                case EffectSource.Environment:
//                    AddReward(-1f);
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
            return config.UseNavMeshForApproachReward
                ? CalculateDistanceWithNavMesh()
                : CalculateDistanceWithManhattanDistance();
        }


        private float CalculateDistanceWithNavMesh()
        {
            // some default distance to a avoid bumping the reward to infinity when we can't find a path
            const float defaultDistance = 10f;
            var path = new NavMeshPath();
            if (!navMeshAgent.isOnNavMesh || !navMeshAgent.CalculatePath(
                    playerInstanceTracker.Player.transform.position,
                    path
                )) return defaultDistance;

            if (path.status != NavMeshPathStatus.PathComplete) return defaultDistance;

            var distance = 0f;
            for (var i = 0; i < path.corners.Length - 1; i++)
                distance += Vector3.Distance(path.corners[i], path.corners[i + 1]);

            return distance;
        }

        private float CalculateDistanceWithManhattanDistance()
        {
            return Vector3.Distance(playerInstanceTracker.Player.transform.position, transform.position);
        }

        private static int PlayerInputToMachineInput(float input)
        {
            if (FloatUtil.NearlyEqual(input, 0)) return (int) MachineInput.NoAction;

            return input > 0 ? (int) MachineInput.PositiveAction : (int) MachineInput.NegativeAction;
        }

        private static int MachineInputToAction(int input)
        {
            ActionFlag machineInputAsAction;
            switch (input)
            {
                case (int) MachineInput.NoAction:
                    machineInputAsAction = ActionFlag.NoAction;
                    break;
                case (int) MachineInput.PositiveAction:
                    machineInputAsAction = ActionFlag.PositiveAction;
                    break;
                case (int) MachineInput.NegativeAction:
                    machineInputAsAction = ActionFlag.NegativeAction;
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"input: {input} is not valid, it must be between 0-2");
            }

            return (int) machineInputAsAction;
        }

        private static bool IsInValidInput(float input)
        {
            return float.IsNaN(input) || float.IsInfinity(input);
        }

        private enum MachineInput
        {
            PositiveAction = 2,
            NegativeAction = 1,
            NoAction = 0
        }

        private enum ActionFlag
        {
            PositiveAction = 1,
            NegativeAction = -1,
            NoAction = 0
        }
    }
}