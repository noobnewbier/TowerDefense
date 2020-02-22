using AgentAi.Manager;
using Elements.Units.UnitCommon;
using UnityEngine;

namespace AgentAi
{
    public class DummyAgent : MonoBehaviour, ICanObserveEnvironment
    {
        [SerializeField] private Unit unit;
        [SerializeField] private EnemyAgentObservationService observationService;
        
        public Texture2D GetObservation() => observationService.CreateObservationAsTexture(unit, null);
    }
}