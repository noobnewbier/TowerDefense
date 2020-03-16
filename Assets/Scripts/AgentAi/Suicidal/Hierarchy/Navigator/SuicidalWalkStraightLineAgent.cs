using System;
using AgentAi.Manager;
using AgentAi.Suicidal.Hierarchy.Config;
using AgentAi.Suicidal.Hierarchy.Event;
using Common.Interface;
using Elements.Units.UnitCommon;
using EventManagement;
using Experimental;
using MLAgents;
using Movement.InputSource;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils;
using UnityUtils.Timers;

namespace AgentAi.Suicidal.Hierarchy.Navigator
{
    public class SuicidalWalkStraightLineAgent : Agent, ICanObserveEnvironment, INavigator
    {
        private IDynamicObjectOfInterest _currentTarget;
        private IEventAggregator _localEventAggregator;
        private IObserveEnvironmentService _observeEnvironmentService;
        private float _previousClosestDistance;
        private Unit _unit;
        private IUnitDataRepository _unitDataRepository;

        [SerializeField] private SuicidalWalkStraightLineConfig config;
        [SerializeField] private ThresholdTimer contactWithObstacleTimer;
        [SerializeField] private AiMovementInputService inputService;
        [SerializeField] private LocalEventAggregatorProvider localEventAggregatorProvider;
        [SerializeField] private ObservationServiceProvider observationServiceProvider;

        [FormerlySerializedAs("provider")] [SerializeField]
        private UnitProvider unitProvider;

        public Texture2D GetObservation()
        {
            return _observeEnvironmentService.CreateObservationAsTexture(_unit, _currentTarget);
        }

        public void Handle(NewTargetIssuedEvent @event)
        {
            _currentTarget = @event.Target;
            
            //stop requesting decision if there is no target, vice versa
            agentParameters.onDemandDecision = _currentTarget == null;
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
            _localEventAggregator = localEventAggregatorProvider.ProvideEventAggregator();
            _previousClosestDistance = GetCurrentDistanceFromTarget();
            _unitDataRepository = unitProvider.ProvideUnitDataRepository();
            _unit = unitProvider.ProvideUnit();
            _observeEnvironmentService = observationServiceProvider.ProvideService();

            _localEventAggregator.Subscribe(this);
            _localEventAggregator.Publish(new RequestNewTargetEvent());
            _localEventAggregator.Publish(new SubAgentSpawnedEvent());
        }

        public override void AgentOnDone()
        {
            base.AgentOnDone();

            _localEventAggregator.Publish(new SubAgentDoneEvent());
        }

        public override void AgentAction(float[] vectorAction)
        {
            var xAction = IsInValidInput(vectorAction[0]) ? 0 : (int) vectorAction[0];
            var yAction = IsInValidInput(vectorAction[1]) ? 0 : (int) vectorAction[1];

            inputService.UpdateVertical(MachineInputToAction(xAction));
            inputService.UpdateHorizontal(MachineInputToAction(yAction));

            PunishRoaming();
            EncourageApproachingTarget();
            CheckReachedTarget();

            //should not keep bumping into an obstacle, something gone wrong my planner
            if (contactWithObstacleTimer.TryResetIfPassedThreshold())
            {
                _localEventAggregator.Publish(new FinishPathingEvent(false));
                _localEventAggregator.Publish(new RequestNewTargetEvent());
            }

            Debug.Log(GetCumulativeReward());
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
            var distance = Vector3.Distance(_currentTarget.ObjectTransform.position, _unit.ObjectTransform.position);

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

        /// <summary>
        ///     call done when reached target: HOWEVER it's not the case when we are not training. It's only in training mode that
        ///     this guy only have one destination
        /// </summary>
        private void CheckReachedTarget()
        {
            if (_unit.Bounds.Intersects(_currentTarget.Bounds))
            {
#if TRAINING
                Done();
#else
                _localEventAggregator.Publish(new RequestNewTargetEvent());
#endif
            }
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