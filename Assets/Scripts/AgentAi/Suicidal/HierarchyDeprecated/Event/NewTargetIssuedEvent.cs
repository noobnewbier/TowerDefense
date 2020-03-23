using Common.Interface;
using JetBrains.Annotations;

namespace AgentAi.Suicidal.Hierarchy.Event
{
    public class NewTargetIssuedEvent
    {
        public NewTargetIssuedEvent(IDynamicObjectOfInterest target)
        {
            Target = target;
        }

        //null indicating there is no target
        [CanBeNull] public IDynamicObjectOfInterest Target { get; }
    }
}