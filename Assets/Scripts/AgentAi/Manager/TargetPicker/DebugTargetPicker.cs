using Common.Interface;
using UnityEngine;

namespace AgentAi.Manager.TargetPicker
{
    public class DebugTargetPicker : MonoBehaviour, ITargetPicker
    {
        [SerializeField] private DebugTarget debugTarget;

        public IDynamicObjectOfInterest RequestTarget()
        {
            return debugTarget;
        }
    }
}