using Common;
using Common.Class;
using Common.Events;
using EventManagement;
using UnityEngine;
using UnityEngine.Animations;

namespace StateMachine
{
    public class PrepareState : StateMachineBehaviour, IHandle<SetupTimesUpEvent>
    {
        private IEventAggregator _eventAggregator;
        private Animator _stateMachine;
        private static readonly int SetupTimesUp = Animator.StringToHash("SetupTimesUp");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            _eventAggregator = EventAggregatorHolder.Instance;
            _stateMachine = animator;
            _eventAggregator.Subscribe(this);
            
            _eventAggregator.Publish(new PreparationBeginsEvent());
        }
        
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
        {
            base.OnStateExit(animator, stateInfo, layerIndex, controller);
            
            _eventAggregator.Unsubscribe(this);
        }

        public void Handle(SetupTimesUpEvent @event)
        {
            _stateMachine.SetTrigger(SetupTimesUp);
        }
    }
}