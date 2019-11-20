using Common.Enum;
using Common.Interface;
using UnityEngine;

namespace Terrain
{
    public class Ground : MonoBehaviour, IObjectOfInterest
    {
        public AiInterestedObjectType InterestedObjectType => AiInterestedObjectType.Ground;
        public Bounds Bounds => GetComponent<Collider>().bounds;
    }
}