using AgentAi.Manager;
using Elements.Units.UnitCommon;
using UnityEngine;

namespace AgentAi
{
    public class DummyAgent : MonoBehaviour, ICanObserveEnvironment
    {
        [SerializeField] private EnemyAgentObservationService observationService;
        [SerializeField] private Unit unit;

        public Texture2D GetObservation()
        {
            return observationService.CreateObservationAsTexture(unit, null);
        }
    }
}