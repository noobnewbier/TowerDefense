using AgentAi.Suicidal.HierarchyDeprecated.Event;
using EventManagement;

namespace AgentAi.Suicidal.HierarchyDeprecated.TargetPicker
{
    public interface ITargetPicker : IHandle<FinishPathingEvent>, IHandle<RequestNewTargetEvent>
    {
    }
}