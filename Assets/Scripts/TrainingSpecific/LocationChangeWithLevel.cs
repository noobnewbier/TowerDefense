using AgentAi;
using Common.Class;
using EventManagement;
using UnityEngine;

namespace TrainingSpecific
{
    public class LocationChangeWithLevel : MonoBehaviour, IHandle<ResetAcademyEvent>
    {
        // need to be length of threshold in curriculum + 1
        [SerializeField] private Transform[] positions;
        private IEventAggregator _eventAggregator;

        private void OnEnable()
        {
            _eventAggregator = EventAggregatorHolder.Instance;
            
            _eventAggregator.Subscribe(this);
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }


        public void Handle(ResetAcademyEvent @event)
        {
            //todo: to be done in a proper way, but lets leave it for now
//            var currentLevel = (int)@event.Academy.resetParameters[VelocityBasedAcademy.EnvironmentParametersKey.Level];
            
//            transform.position = positions[currentLevel].position;
        }
    }
}