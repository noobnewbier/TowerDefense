using Common.Enum;
using Common.Event;
using Common.Interface;
using Experimental;
using UnityEngine;
using UnityEngine.Serialization;

namespace Terrain
{
    public class DynamicObstacle : MonoBehaviour, IDynamicObjectOfInterest
    {
        private Collider _collider;

        [FormerlySerializedAs("scriptableEventAggregator")] [SerializeField]
        private EventAggregatorProvider eventAggregatorProvider;

        public AiInterestCategory InterestCategory => AiInterestCategory.Obstacle;
        public Bounds Bounds => _collider.bounds;
        public Transform ObjectTransform => transform;

        private void OnEnable()
        {
            _collider = GetComponent<Collider>();

            eventAggregatorProvider.ProvideEventAggregator().Publish(new DynamicObstacleSpawnedEvent(this));
        }

        private void OnDisable()
        {
            eventAggregatorProvider.ProvideEventAggregator().Publish(new DynamicObstacleDestroyedEvent(this));
        }
    }
}