using Common.Interface;

namespace AgentAi.Suicidal.Hierarchy.Event
{
    public class NewTargetIssuedEvent
    {
        public NewTargetIssuedEvent(IDynamicObjectOfInterest target)
        {
            Target = target;
        }

        public IDynamicObjectOfInterest Target { get; }
    }
}