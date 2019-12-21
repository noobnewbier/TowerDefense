using AgentAi.VelocityBasedAgent;
using Common.Class;
using EventManagement;
using UnityEngine;
using UnityUtils.LocationProviders;

namespace TrainingSpecific
{
    public class LocationChangeWithLevelProvider : LocationProvider, IHandle<ResetAcademyEvent>
    {
        // need to be length of threshold in curriculum + 1
        [SerializeField] private LocationProvider[] positions;
        private IEventAggregator _eventAggregator;
        private int _currentLevel;

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
            _currentLevel = (int)@event.Academy.resetParameters[VelocityBasedAcademy.EnvironmentParametersKey.Level];            
        }

        public override Vector3 ProvideLocation() => positions[_currentLevel].ProvideLocation();
    }
}