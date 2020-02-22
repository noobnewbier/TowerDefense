using AgentAi.Manager;
using UnityEngine;

namespace AgentAi
{
    public class ObservationServiceProvider : MonoBehaviour
    {
        [SerializeField] private EnemyAgentObservationService observationService;

        public IObserveEnvironmentService ProvideService()
        {
            return observationService;
        }
    }
}