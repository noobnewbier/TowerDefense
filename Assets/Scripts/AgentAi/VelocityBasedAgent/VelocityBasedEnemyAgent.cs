using System;
using System.Diagnostics.CodeAnalysis;
using AgentAi.Manager;
using Common.Class;
using Common.Constant;
using Common.Enum;
using Common.Event;
using Elements.Units.Enemies;
using EventManagement;
using MLAgents;
using Movement.InputSource;
using TrainingSpecific.Events;
using UnityEngine;
using UnityEngine.AI;
using UnityUtils;

namespace AgentAi.VelocityBasedAgent
{
    //todo: consider refactoring, it feels like this is doing too much. Consider outsourcing reward calculation
    public class VelocityBasedEnemyAgent : Agent, IHandle<EnemyDeadEvent>, ICanObserveEnvironment
    {
        private const float RoamingPunishment = -0.01f;

        private IEventAggregator _eventAggregator;
        private IObserveEnvironmentService _observeEnvironmentService;
        private float _previousClosestDistance;
        private float _initialDistanceToTarget;
        private float _totalRewardForApproachingConsideringTimeRequired;
        private ITargetPicker _targetPicker;

        [SerializeField] private AiMovementInputService inputService;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private VelocityBasedEnemy unit;

        public Texture2D GetObservation() => _observeEnvironmentService.CreateObservationAsTexture(unit, _targetPicker.Target);

        public void Handle(EnemyDeadEvent @event)
        {
            if (@event.Enemy != unit)
            {
                return;
            }

            RewardIsDead(@event.DeathCause);
        }

        //cannot think of an elegant solution for heuristic controller... but this do the trick
        public override float[] Heuristic() => new float[]
        {
            PlayerInputToMachineInput(Input.GetAxis("Vertical")),
            PlayerInputToMachineInput(Input.GetAxis("Horizontal"))
        };

        public override void InitializeAgent()
        {
            base.InitializeAgent();
            _eventAggregator = EventAggregatorHolder.Instance;
            _targetPicker = TargetPicker.Instance;
            _observeEnvironmentService = EnemyAgentObservationCollector.Instance;

            RewardCalculation();
            
            _eventAggregator.Subscribe(this);
            _eventAggregator.Publish(new AgentSpawnedEvent());
        }

        private void RewardCalculation()
        {
            _initialDistanceToTarget= GetCurrentDistanceFromTarget();
            _previousClosestDistance = _initialDistanceToTarget;
            var minimumPunishmentFromRoaming = GetMinimumPunishmentFromRoaming();
            _totalRewardForApproachingConsideringTimeRequired = GetTotalApproachingRewardAfterConsideringTimeRequired(minimumPunishmentFromRoaming);            
        }

        private float GetMinimumPunishmentFromRoaming()
        {
            //ignoring the acceleration phase... It's not significant, and frankly you don't know how to calculate it
            var shortestTimeToTargetInSec = _initialDistanceToTarget / unit.MaxSpeed;
            return shortestTimeToTargetInSec / Time.fixedDeltaTime * RoamingPunishment / agentParameters.numberOfActionsBetweenDecisions;
        }

        private static float GetTotalApproachingRewardAfterConsideringTimeRequired(float minimumPunishmentFromRoaming)
        {
            //maximum reward you get by "approaching" is 2, you need to get the remaining 1 by actually touching the target
            const float rewardForApproaching = 2f;
            
            return rewardForApproaching + minimumPunishmentFromRoaming;
        }

        public override void AgentOnDone()
        {
            base.AgentOnDone();

            _eventAggregator.Publish(new AgentDoneEvent());
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            var xAction = IsInValidInput(vectorAction[0]) ? 0 : (int) vectorAction[0];
            var yAction = IsInValidInput(vectorAction[1]) ? 0 : (int) vectorAction[1];

            inputService.UpdateVertical(MachineInputToAction(xAction));
            inputService.UpdateHorizontal(MachineInputToAction(yAction));

            PunishRoaming();
            EncourageApproachingTarget();
        }

        private void OnCollisionStay(Collision other)
        {
            if (other.collider.CompareTag(ObjectTags.Wall))
            {
                AddReward(-0.15f);
            }
        }

        [SuppressMessage("ReSharper", "RedundantCaseLabel")]
        private void RewardIsDead(DamageSource deathCause)
        {
            switch (deathCause)
            {
                case DamageSource.Player:
                    AddReward(-0.15f);
                    break;
                case DamageSource.System:
                    AddReward(-1f);
                    break;
                case DamageSource.SelfDestruction:
                    AddReward(1f);
                    break;
                case DamageSource.Ai:
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Done();
        }

        protected override void DisposeAgent()
        {
            base.DisposeAgent();

            _eventAggregator.Unsubscribe(this);
        }

        //Don't walk around forever pls
        private void PunishRoaming()
        {
            AddReward(RoamingPunishment);
        }

        private void EncourageApproachingTarget()
        {
            var distance = GetCurrentDistanceFromTarget();

            if (distance < _previousClosestDistance)
            {
                var distanceDifference = _previousClosestDistance - distance;

                AddReward(_totalRewardForApproachingConsideringTimeRequired * (distanceDifference / _initialDistanceToTarget));
                _previousClosestDistance = distance;
            }
        }

        private float GetCurrentDistanceFromTarget()
        {
            // some default distance to a avoid bumping the reward to infinity when we can't find a path
            const float defaultDistance = 5f;
            var path = new NavMeshPath();
            if (!navMeshAgent.isOnNavMesh || !navMeshAgent.CalculatePath(_targetPicker.Target.DynamicObjectTransform.position, path))
            {
                return defaultDistance;
            }

            if (path.status != NavMeshPathStatus.PathComplete)
            {
                return defaultDistance;
            }

            var distance = 0f;
            for (var i = 0; i < path.corners.Length - 1; i++) distance += Vector3.Distance(path.corners[i], path.corners[i + 1]);

            return distance;
        }

        private static int PlayerInputToMachineInput(float input)
        {
            if (FloatUtil.NearlyEqual(input, 0))
            {
                return (int) MachineInput.NoAction;
            }

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

        private static bool IsInValidInput(float input) => float.IsNaN(input) || float.IsInfinity(input);

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