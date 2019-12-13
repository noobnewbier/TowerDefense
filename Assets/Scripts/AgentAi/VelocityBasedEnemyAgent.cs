using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
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
using UnityUtils;

namespace AgentAi
{
    public class VelocityBasedEnemyAgent : Agent, IHandle<EnemyDeadEvent>
    {
        private IEventAggregator _eventAggregator;
        [SerializeField] private AiMovementInputService inputService;
        [SerializeField] private VelocityBasedEnemy unit;

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

            EncourageApproachingPlayer();
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

        private void Awake()
        {
            _eventAggregator = EventAggregatorHolder.Instance;
            _eventAggregator.Subscribe(this);
        }

        private void OnDestroy()
        {
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
        private void EncourageApproachingPlayer()
        {
            AddReward(-0.001f);
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
                    throw new ArgumentOutOfRangeException($"input: {input} is not valid, it must between 0-2");
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