using Common.Enum;
using Common.Event;
using Common.Interface;
using Common.Struct;
using EventManagement;
using EventManagement.Providers;
using Experimental;
using UnityEngine;

namespace AgentAi.Suicidal.TargetProviders
{
    public class TargetMarker : MonoBehaviour, IDynamicObjectOfInterest
    {
        private IEventAggregator _eventAggregator;

        [SerializeField] private EventAggregatorProvider eventAggregatorProvider;
        [Range(0.001f, 10)] [SerializeField] private float size;
        private Bounds Bounds => new Bounds(transform.position, new Vector3(size, size, size));

        public InterestedInformation InterestedInformation =>
            new InterestedInformation(InterestCategory.System, Bounds);

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

        private void OnDrawGizmos()
        {
            var originalColor = Gizmos.color;

            Gizmos.color = Color.cyan;
            Gizmos.DrawCube(transform.position, Bounds.size);

            Gizmos.color = originalColor;
        }
    }
}