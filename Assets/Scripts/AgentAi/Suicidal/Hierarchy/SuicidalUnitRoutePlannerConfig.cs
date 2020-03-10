using UnityEngine;

namespace AgentAi.Suicidal.Hierarchy
{
    [CreateAssetMenu(menuName = "AIConfig/SuicidalUnit")]
    public class SuicidalUnitRoutePlannerConfig : ScriptableObject
    {
        [SerializeField] private float killedPunishment;
        [SerializeField] private float maxApproachReward;
        [SerializeField] private float roamingPunishment;
        [SerializeField] private float selfDestructionReward;
        [SerializeField] private bool useVectorRotation;

        public float RoamingPunishment => roamingPunishment;
        public bool UseVectorRotation => useVectorRotation;
        public float MaxApproachReward => maxApproachReward;
        public float SelfDestructionReward => selfDestructionReward;
        public float KilledPunishment => killedPunishment;
    }
}