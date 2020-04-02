using UnityEngine;

namespace AgentAi.Suicidal.HierarchyDeprecated.Configs
{
    [CreateAssetMenu(menuName = "AIConfig/RoutePlannerConfig")]
    public class SuicidalUnitRoutePlannerConfig : ScriptableObject
    {
        [SerializeField] private float arrivedTargetReward;
        [SerializeField] private float collisionPunishment;
        [SerializeField] private float killedPunishment;
        [SerializeField] private float roamingPunishment;
        [SerializeField] private float selfDestructionReward;

        public float ArrivedTargetReward => arrivedTargetReward;
        public float CollisionPunishment => collisionPunishment;
        public float RoamingPunishment => roamingPunishment;
        public float SelfDestructionReward => selfDestructionReward;
        public float KilledPunishment => killedPunishment;
    }
}