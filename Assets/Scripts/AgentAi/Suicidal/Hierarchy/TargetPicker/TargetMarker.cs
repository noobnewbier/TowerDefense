using Common.Enum;
using Common.Event;
using Common.Interface;
using EventManagement;
using Experimental;
using UnityEngine;

namespace AgentAi.Suicidal.Hierarchy.TargetPicker
{
    public class TargetMarker : MonoBehaviour, IDynamicObjectOfInterest
    {
        private IEventAggregator _eventAggregator;
        [SerializeField] private EventAggregatorProvider eventAggregatorProvider;
        [Range(1, 10)] [SerializeField] private float size;
        public AiInterestCategory InterestCategory => AiInterestCategory.System;
        public Bounds Bounds => new Bounds(transform.position, new Vector3(size, size, size));
        public Transform ObjectTransform => transform;

        private void OnEnable()
        {
            _eventAggregator = eventAggregatorProvider.ProvideEventAggregator();

            _eventAggregator.Publish(new SystemObjectSpawnedEvent(this));
        }

        private void OnDisable()
        {
            _eventAggregator.Publish(new SystemObjectDestroyedEvent(this));
        }
    }
}