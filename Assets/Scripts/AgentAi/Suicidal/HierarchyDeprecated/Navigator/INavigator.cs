using AgentAi.Suicidal.HierarchyDeprecated.Event;
using EventManagement;

namespace AgentAi.Suicidal.HierarchyDeprecated.Navigator
{
    public interface INavigator : IHandle<NewTargetIssuedEvent>
    {
    }
}