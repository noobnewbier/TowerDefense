using Common.Enum;
using Common.Interface;
using UnityEngine;

namespace AgentAi.Manager.TargetPicker
{
    public class DebugTarget : MonoBehaviour, IDynamicObjectOfInterest
    {
        [Range(1, 10)] [SerializeField] private float size;
        public AiInterestCategory InterestCategory => AiInterestCategory.System;
        public Bounds Bounds => new Bounds(transform.position, new Vector3(size, size, size));
        public Transform DynamicObjectTransform => transform;
    }
}