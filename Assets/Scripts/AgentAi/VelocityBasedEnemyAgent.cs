using System;
using System.Diagnostics.CodeAnalysis;
using Common;
using Common.Enum;
using MLAgents;
using Movement.InputSource;
using Units.UnitCommon;
using UnityEngine;

namespace AgentAi
{
    public class VelocityBasedEnemyAgent : Agent
    {
        [SerializeField] private AiMovementInputService inputService;
        [SerializeField] private Unit unit;

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            base.AgentAction(vectorAction, textAction);

            inputService.UpdateVertical(vectorAction[0]);
            inputService.UpdateHorizontal(vectorAction[1]);
            
            RewardIsDead();
            EncourageApproachingPlayer();
        }

        [SuppressMessage("ReSharper", "RedundantCaseLabel")]
        private void RewardIsDead()
        {
            if (!unit.IsDead) return;
            
            switch (unit.DeathCause)
            {
                case DamageSource.Player:
                case DamageSource.Environment:
                    AddReward(-1f);
                    break;
                case DamageSource.SelfDestruction:
                    AddReward(1f);
                    break;
                case DamageSource.Ai:
                case null:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        //Don't walk around forever pls
        private void EncourageApproachingPlayer()
        {
            AddReward(-0.0001f);
        }
    }
}