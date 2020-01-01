using System;
using Common.Class;
using Common.Enum;
using Common.Event;
using Common.Interface;
using Experimental;
using UnityEngine;

namespace Terrain
{
    public class DynamicObstacle : MonoBehaviour, IDynamicObjectOfInterest
    {
        [SerializeField] private ScriptableEventAggregator scriptableEventAggregator;
        
        public AiInterestCategory InterestCategory => AiInterestCategory.Obstacle;
        public Bounds Bounds => _collider.bounds;
        public Transform DynamicObjectTransform => transform;
        private Collider _collider;

        private void OnEnable()
        {
            _collider = GetComponent<Collider>();
            
            scriptableEventAggregator.Instance.Publish(new DynamicObstacleSpawnedEvent(this));
        }

        private void OnDisable()
        {
            scriptableEventAggregator.Instance.Publish(new DynamicObstacleDestroyedEvent(this));
        }
    }
}