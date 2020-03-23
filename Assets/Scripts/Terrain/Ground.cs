using Common.Class;
using Common.Enum;
using Common.Interface;
using Common.Struct;
using UnityEngine;
using UnityEngine.Serialization;

namespace Terrain
{
    public class Ground : MonoBehaviour, IStaticObjectOfInterest
    {
        public InterestedInformation InterestedInformation { get; private set; }
        public Transform ObjectTransform => transform;

        private void OnEnable()
        {
            InterestedInformation = new InterestedInformation(InterestCategory.Ground, GetComponent<Collider>().bounds);
        }
    }
}