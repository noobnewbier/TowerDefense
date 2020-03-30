using AgentAi.Suicidal.HierarchyDeprecated.TargetPicker;
using Common.Interface;
using UnityEngine;

namespace AgentAi.Suicidal.TargetProviders
{
    public class DebugTargetProvider : TargetProvider
    {
        [SerializeField] private TargetMarker targetMarker;
        public override IDynamicObjectOfInterest Target => targetMarker;
    }
}