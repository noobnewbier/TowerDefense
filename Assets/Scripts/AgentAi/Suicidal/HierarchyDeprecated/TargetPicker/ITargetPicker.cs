using AgentAi.Suicidal.Hierarchy.Event;
using EventManagement;

namespace AgentAi.Suicidal.Hierarchy.TargetPicker
{
    public interface ITargetPicker : IHandle<FinishPathingEvent>, IHandle<RequestNewTargetEvent>
    {
    }
}