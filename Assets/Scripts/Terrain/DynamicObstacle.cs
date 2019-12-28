using Common.Enum;
using Common.Interface;
using UnityEngine;
using UnityEngine.AI;

namespace Terrain
{
    public class DynamicObstacle : MonoBehaviour, IDynamicObjectOfInterest
    {
        public AiInterestCategory InterestCategory => AiInterestCategory.Obstacle;
        public Bounds Bounds => _collider.bounds;
        public Transform DynamicObjectTransform => transform;

        private Collider _collider;

        private void OnEnable()
        {
            _collider = GetComponent<Collider>();
        }
    }
}