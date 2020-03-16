using AgentAi.Manager;
using AgentAi.Suicidal.Hierarchy.Event;
using Elements.Units.UnitCommon;
using EventManagement;
using Experimental;
using UnityEngine;

namespace AgentAi.Suicidal.Hierarchy.TargetPicker
{
    public class DebugTargetPicker : MonoBehaviour, ITargetPicker, ICanObserveEnvironment
    {
        private TargetMarker _targetMarker;
        private IEventAggregator _eventAggregator;
        private IObserveEnvironmentService _observeEnvironmentService;
        private Unit _unit;
        [SerializeField] private ObservationServiceProvider observationServiceProvider;
        [SerializeField] private LocalEventAggregatorProvider localEventAggregatorProvider;
        [SerializeField] private GameObject targetMarkerPrefab;
        [SerializeField] private UnitProvider unitProvider;
        

        public void Handle(FinishPathingEvent @event)
        {
            //do nothing
        }

        public void Handle(RequestNewTargetEvent @event)
        {
            _eventAggregator.Publish(new NewTargetIssuedEvent(_targetMarker));
        }

        private void OnEnable()
        {
            _eventAggregator = localEventAggregatorProvider.ProvideEventAggregator();
            _observeEnvironmentService = observationServiceProvider.ProvideService();
            _targetMarker = Instantiate(targetMarkerPrefab).GetComponent<TargetMarker>();
            _unit = unitProvider.ProvideUnit();

            _eventAggregator.Subscribe(this);
        }

        private void OnDisable()
        {
            _eventAggregator.Publish(new NewTargetIssuedEvent(null));
            _eventAggregator.Unsubscribe(this);
            
            Destroy(_targetMarker.gameObject);
        }

        public Texture2D GetObservation()
        {
            return _observeEnvironmentService.CreateObservationAsTexture(_unit, null);
        }
    }
}