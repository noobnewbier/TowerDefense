using System;
using System.Diagnostics.CodeAnalysis;
using Common.Class;
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
        [SerializeField] private AiMovementInputService inputService;
        [SerializeField] private VelocityBasedEnemy unit;

        private IEventAggregator _eventAggregator;

        public void Handle(EnemyDeadEvent @event)
        {
            if (@event.Unit != unit) return;
            RewardIsDead(@event.DeathCause);
        }

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

        public override void AgentAction(float[] vectorAction, string textAction)
        {   
            inputService.UpdateVertical(vectorAction[0]);
            inputService.UpdateHorizontal(vectorAction[1]);

            EncourageApproachingPlayer();
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


        public override void AgentOnDone()
        {
            base.AgentOnDone();
            
            _eventAggregator.Publish(new AgentDoneEvent());
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
            AddReward(-0.0001f);
        }
    }
}