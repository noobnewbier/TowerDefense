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
using UnityUtils;

namespace AgentAi.Suicidal.Hierarchy
{
    public class SuicidalWalkStraightLineAgent : Agent, IHandle<EnemyDeadEvent>, ICanObserveEnvironment
    {
        private IDynamicObjectOfInterest _currentTarget;
        private IEventAggregator _eventAggregator;
        private IObserveEnvironmentService _observeEnvironmentService;
        private float _previousClosestDistance;
        private ITargetPicker _targetPicker;
        private IUnitDataRepository _unitDataRepository;

        [SerializeField] private SuicidalWalkStraightLineConfig config;
        [SerializeField] private AiMovementInputService inputService;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private ObservationServiceProvider observationServiceProvider;
        [SerializeField] private UnitProvider provider;
        [SerializeField] private SuicidalEnemy unit;

        public Texture2D GetObservation()
        {
            return _observeEnvironmentService.CreateObservationAsTexture(unit, _currentTarget);
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
                PlayerInputToMachineInput(Input.GetAxis("Vertical")),
                PlayerInputToMachineInput(Input.GetAxis("Horizontal"))
            };
        }

        public override void InitializeAgent()
        {
            base.InitializeAgent();
            _eventAggregator = EventAggregatorHolder.Instance;
            _targetPicker = PickPlayerTargetPicker.Instance;
            _previousClosestDistance = GetCurrentDistanceFromTarget();
            _unitDataRepository = provider.ProvideUnitDataRepository();
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
            var xAction = IsInValidInput(vectorAction[0]) ? 0 : (int) vectorAction[0];
            var yAction = IsInValidInput(vectorAction[1]) ? 0 : (int) vectorAction[1];

            inputService.UpdateVertical(MachineInputToAction(xAction));
            inputService.UpdateHorizontal(MachineInputToAction(yAction));
            PunishRoaming();
            EncourageApproachingTarget();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<IDynamicObjectOfInterest>() == _currentTarget)
                _currentTarget = _targetPicker.RequestTarget();
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