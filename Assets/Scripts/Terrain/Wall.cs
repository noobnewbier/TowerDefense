using Common.Enum;
using Common.Interface;
using UnityEngine;

namespace Terrain
{
    public class Wall : MonoBehaviour, IObjectOfInterest
    {
        public AiInterestedObjectType InterestedObjectType => AiInterestedObjectType.Obstacle;
        public Bounds Bounds => GetComponent<Collider>().bounds;
    }
}