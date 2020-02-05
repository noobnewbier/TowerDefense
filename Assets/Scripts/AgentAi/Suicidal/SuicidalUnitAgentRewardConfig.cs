using UnityEngine;

namespace AgentAi.Suicidal
{
    [CreateAssetMenu(menuName = "AIConfig/SuicidalUnitReward")]
    public class SuicidalUnitAgentRewardConfig : ScriptableObject
    {
        [SerializeField] private float roamingPunishment;
        [SerializeField] private float maxApproachReward;
        [SerializeField] private float selfDestructionReward;
        [SerializeField] private float killedPunishment;
        [SerializeField] private float contactWithObstaclePunishment;

        public float RoamingPunishment => roamingPunishment;

        public float MaxApproachReward => maxApproachReward;

        public float SelfDestructionReward => selfDestructionReward;

        public float KilledPunishment => killedPunishment;

        public float ContactWithObstaclePunishment => contactWithObstaclePunishment;
    }
}