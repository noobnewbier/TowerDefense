using Common.Class;
using EventManagement;
using MLAgents;

namespace AgentAi
{
    public class VelocityBasedAcademy : Academy
    {
        private IEventAggregator _eventAggregator;
        
        private void OnEnable()
        {
            _eventAggregator = EventAggregatorHolder.Instance;
        }
    }
}