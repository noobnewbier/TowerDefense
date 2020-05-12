using UnityEngine;
using UnityEngine.Serialization;

namespace AgentAi.Suicidal
{
    [CreateAssetMenu(menuName = "AIConfig/SuicidalUnit")]
    public class SuicidalUnitAgentConfig : ScriptableObject
    {
        [SerializeField] private float contactWithObstaclePunishment;
        [FormerlySerializedAs("damagePunishment")] [SerializeField] private float perDamagePunishment = -0.05f;
        [SerializeField] private float killedPunishment;
        [SerializeField] private float maxApproachReward;
        [SerializeField] private float roamingPunishment;
        [SerializeField] private float selfDestructionReward;
        [SerializeField] private bool useContinuousOutput;
        [SerializeField] private bool useNavMeshForApproachReward = true;
        [SerializeField] private bool useVectorRotation;

        public float PerDamagePunishment => perDamagePunishment;
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