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
using TrainingSpecific;
using UnityEngine;
using UnityEngine.AI;
using UnityUtils;

namespace AgentAi.VelocityBasedAgent
{
    public class VelocityBasedEnemyAgent : Agent, IHandle<EnemyDeadEvent>, ICanObserveEnvironment
    {
        private IEventAggregator _eventAggregator;
        private IObserveEnvironmentService _observeEnvironmentService;
        private ITargetPicker _targetPicker;
        private float _previousClosestDistance;
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

        //cannot think of an elegant solution here... but this do the trick
        public override float[] Heuristic() => new float[]
        {
            PlayerInputToMachineInput(Input.GetAxis("Vertical")),
            PlayerInputToMachineInput(Input.GetAxis("Horizontal"))
        };

        public override void CollectObservations()
        {
            AddQuaternions();
//            AddVelocity();
        }

        public override void InitializeAgent()
        {
            base.InitializeAgent();
            _eventAggregator = EventAggregatorHolder.Instance;
            _targetPicker = TargetPicker.Instance;
            _observeEnvironmentService = EnemyAgentObservationCollector.Instance;
            _eventAggregator.Subscribe(this);

            _eventAggregator.Publish(new AgentSpawnedEvent());
        }

        public override void AgentOnDone()
        {
            base.AgentOnDone();

            _eventAggregator.Publish(new AgentDoneEvent());
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            inputService.UpdateVertical(MachineInputToAction((int) vectorAction[0]));
            inputService.UpdateHorizontal(MachineInputToAction((int) vectorAction[1]));

            PunishWonderingWithNoReason();
            EncourageApproachingTarget();
        }

        private void OnCollisionStay(Collision other)
        {
            if (other.collider.CompareTag(ObjectTags.Wall))
            {
                AddReward(-0.005f);
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

        private void AddQuaternions()
        {
            AddVectorObs(unit.Rigidbody.rotation);
        }

        private void AddVelocity()
        {
            AddVectorObs(unit.Rigidbody.velocity);
        }

        //Don't walk around forever pls
        private void PunishWonderingWithNoReason()
        {
            AddReward(-0.001f);
        }

        private void EncourageApproachingTarget()
        {
            const float reward = 0.1f;

            var path = new NavMeshPath();
            if (!navMeshAgent.CalculatePath(_targetPicker.Target.Transform.position, path))
            {
                return;
            }

            if (path.status != NavMeshPathStatus.PathComplete)
            {
                return;
            }

            var distance = 0f;
            for (var i = 0; i < path.corners.Length - 1; i++) distance += Vector3.Distance(path.corners[i], path.corners[i + 1]);

            if (distance < _previousClosestDistance)
            {
                // TODO: we are assuming that the unit's maximum achievement is the maximum distance that it can move in one decision. Is that correct?
                var maximumAchievement = unit.MaxSpeed * Time.fixedDeltaTime * agentParameters.numberOfActionsBetweenDecisions;
                var distanceDifference = _previousClosestDistance - distance;

                Debug.Log(reward * (distanceDifference / maximumAchievement));

                AddReward(reward * (distanceDifference / maximumAchievement));
                _previousClosestDistance = distance;
            }
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