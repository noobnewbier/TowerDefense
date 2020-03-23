using Common.Enum;
using Common.Interface;
using Common.Struct;
using UnityEngine;

namespace Terrain
{
    public class Wall : MonoBehaviour, IStaticObjectOfInterest
    {
        public InterestedInformation InterestedInformation { get; private set; }


        public Transform ObjectTransform => transform;

        private void OnEnable()
        {
            InterestedInformation = new InterestedInformation(InterestCategory.Obstacle, GetComponent<Collider>().bounds);
        }
    }
}