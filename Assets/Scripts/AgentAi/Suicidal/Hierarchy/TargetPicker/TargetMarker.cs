using Common.Enum;
using Common.Interface;
using UnityEngine;

namespace AgentAi.Suicidal.Hierarchy.TargetPicker
{
    public class TargetMarker : MonoBehaviour, IDynamicObjectOfInterest
    {
        [Range(1, 10)] [SerializeField] private float size;
        public AiInterestCategory InterestCategory => AiInterestCategory.System;
        public Bounds Bounds => new Bounds(transform.position, new Vector3(size, size, size));
        public Transform ObjectTransform => transform;
    }
}