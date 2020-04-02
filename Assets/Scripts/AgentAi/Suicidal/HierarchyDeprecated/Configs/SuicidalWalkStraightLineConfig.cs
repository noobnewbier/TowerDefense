using UnityEngine;

namespace AgentAi.Suicidal.HierarchyDeprecated.Configs
{
    [CreateAssetMenu(menuName = "AIConfig/WalkStraightLineConfig")]
    public class SuicidalWalkStraightLineConfig : ScriptableObject
    {
        [SerializeField] private float killedPunishment;
        [SerializeField] private float maxApproachReward;
        [SerializeField] private float roamingPunishment;
        [SerializeField] private float selfDestructionReward;

        public float RoamingPunishment => roamingPunishment;
        public float MaxApproachReward => maxApproachReward;
        public float SelfDestructionReward => selfDestructionReward;
        public float KilledPunishment => killedPunishment;
    }
}