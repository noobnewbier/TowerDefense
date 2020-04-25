using UnityEngine;

namespace AgentAi.Suicidal
{
    [CreateAssetMenu(menuName = "AIConfig/SuicidalUnit")]
    public class SuicidalUnitAgentConfig : ScriptableObject
    {
        [SerializeField] private float contactWithObstaclePunishment;
        [SerializeField] private float killedPunishment;
        [SerializeField] private float maxApproachReward;
        [SerializeField] private float roamingPunishment;
        [SerializeField] private float selfDestructionReward;
        [SerializeField] private bool useVectorRotation;
        [SerializeField] private bool useNavMeshForApproachReward = true;
        [SerializeField] private bool useContinuousOutput;

        public bool UseContinuousOutput => useContinuousOutput;
        public bool UseNavMeshForApproachReward => useNavMeshForApproachReward;
        public float RoamingPunishment => roamingPunishment;
        public bool UseVectorRotation => useVectorRotation;
        public float MaxApproachReward => maxApproachReward;

        public float SelfDestructionReward => selfDestructionReward;

        public float KilledPunishment => killedPunishment;

        public float ContactWithObstaclePunishment => contactWithObstaclePunishment;
    }
}