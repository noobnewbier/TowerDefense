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
        public override float[] Heuristic() => new float[2] {Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")};

        public override void CollectObservations()
        {
            AddQuaternions();
            AddVelocity();
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
            inputService.UpdateVertical(Mathf.Clamp(vectorAction[0], -1, 1));
            inputService.UpdateHorizontal(Mathf.Clamp(vectorAction[1], -1, 1));

            EncourageApproachingPlayer();
        }

//        private void OnCollisionStay(Collision other)
//        {
//            if (other.collider.CompareTag(ObjectTags.Wall))
//            {
//                AddReward(-0.005f);
//            }
//        }

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
    }
}