using Common.Enum;
using Common.Interface;
using UnityEngine;

namespace Terrain
{
    public class Wall : MonoBehaviour, IStaticObjectOfInterest
    {
        public AiInterestCategory InterestCategory => AiInterestCategory.Obstacle;
        public Bounds Bounds => GetComponent<Collider>().bounds;
        public Transform Transform => transform;
    }
}