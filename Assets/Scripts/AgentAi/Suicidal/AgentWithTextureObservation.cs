using MLAgents;
using UnityEngine;
using UnityEngine.Serialization;

namespace AgentAi.Suicidal
{
    public class AgentWithTextureObservation : Agent
    {
        [FormerlySerializedAs("rewardConfig")] [SerializeField]
        protected SuicidalUnitAgentConfig config;

        public SuicidalUnitAgentConfig Config => config;
    }
}