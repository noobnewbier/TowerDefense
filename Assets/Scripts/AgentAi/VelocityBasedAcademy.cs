using Common.Class;
using EventManagement;
using MLAgents;
using TrainingSpecific;

namespace AgentAi
{
    public class VelocityBasedAcademy : Academy
    {
        private IEventAggregator _eventAggregator;
        
        private void OnEnable()
        {
            _eventAggregator = EventAggregatorHolder.Instance;
        }

        public override void AcademyReset()
        {
            _eventAggregator.Publish(new ForceResetEvent());
        }
    }
}