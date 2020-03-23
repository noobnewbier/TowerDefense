using AgentAi.Suicidal.Hierarchy.Event;
using EventManagement;

namespace AgentAi.Suicidal.Hierarchy.Navigator
{
    public interface INavigator : IHandle<NewTargetIssuedEvent>
    {
    }
}