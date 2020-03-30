using Common.Interface;
using UnityEngine;

namespace AgentAi.Suicidal.TargetProviders
{
    public abstract class TargetProvider : MonoBehaviour
    {
        public abstract IDynamicObjectOfInterest Target { get; }
    }
}