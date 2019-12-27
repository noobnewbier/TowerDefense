using Common.Enum;
using Common.Interface;
using UnityEngine;
using UnityEngine.AI;

namespace Terrain
{
    public class DynamicObstacle : MonoBehaviour, IDynamicObjectOfInterest
    {
        public AiInterestCategory InterestCategory => AiInterestCategory.Obstacle;
        public Bounds Bounds { get; private set; }
        public Transform DynamicObjectTransform => transform;

        private void OnEnable()
        {
            Bounds = GetComponent<Collider>().bounds;

            GetComponent<NavMeshObstacle>().size = Bounds.size;
        }
    }
}