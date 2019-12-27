using AgentAi.Manager;
using Elements.Units.UnitCommon;
using UnityEngine;

namespace AgentAi
{
    public class DummyAgent : MonoBehaviour, ICanObserveEnvironment
    {
        [SerializeField] private Unit unit;
        private IObserveEnvironmentService _observeEnvironmentService;
        public Texture2D GetObservation() => _observeEnvironmentService.CreateObservationAsTexture(unit, null);

        private void OnEnable()
        {
            _observeEnvironmentService = EnemyAgentObservationCollector.Instance;
        }
    }
}