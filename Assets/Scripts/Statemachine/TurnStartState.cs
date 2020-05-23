using Common.Class;
using EventManagement;
using TrainingSpecific.Events;
using UnityEngine;

namespace Statemachine
{
    public class TurnStartState : StateMachineBehaviour
    {
        private IEventAggregator _eventAggregator;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _eventAggregator = EventAggregatorHolder.Instance;
            
            _eventAggregator.Publish(new TurnStartEvent());
        }
    }
}