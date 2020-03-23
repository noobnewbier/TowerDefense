using Common.Enum;
using Common.Event;
using Common.Interface;
using Common.Struct;
using EventManagement;
using Experimental;
using UnityEngine;

namespace AgentAi.Suicidal.HierarchyDeprecated.TargetPicker
{
    public class TargetMarker : MonoBehaviour, IDynamicObjectOfInterest
    {
        private IEventAggregator _eventAggregator;

        [SerializeField] private EventAggregatorProvider eventAggregatorProvider;
        [Range(1, 10)] [SerializeField] private float size;
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