using Common.Enum;
using Common.Interface;
using UnityEngine;

namespace Terrain
{
    public class Ground : MonoBehaviour, IObjectOfInterest
    {
        public AiInterestCategory InterestCategory => AiInterestCategory.Ground;
        public Bounds Bounds => GetComponent<Collider>().bounds;
    }
}