using Common.Class;
using EventManagement;
using TrainingSpecific.Events;
using UnityEngine;

namespace TrainingSpecific.StateMachine
{
    public class HandleDynamicObstacleState : StateMachineBehaviour, IHandle<DynamicObjectsHandledEvent>
    {
        private static readonly int HandledDynamicObstacle = Animator.StringToHash("HandledDynamicObstacle");
        private IEventAggregator _eventAggregator;
        private Animator _stateMachine;

        public void Handle(DynamicObjectsHandledEvent @event)
        {
            _stateMachine.SetTrigger(HandledDynamicObstacle);
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _stateMachine = animator;
            _eventAggregator = EventAggregatorHolder.Instance;
            _eventAggregator.Subscribe(this);
            
            _eventAggregator.Publish(new TurnStartEvent());
            _eventAggregator.Publish(new HandleDynamicObstacleEvent());
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}