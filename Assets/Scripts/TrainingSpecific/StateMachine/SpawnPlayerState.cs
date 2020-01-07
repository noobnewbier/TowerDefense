using Common.Class;
using Common.Event;
using EventManagement;
using TrainingSpecific.Events;
using UnityEngine;

namespace TrainingSpecific.StateMachine
{
    public class SpawnPlayerState : StateMachineBehaviour, IHandle<PlayerSpawnedEvent>
    {
        private static readonly int SpawnedPlayer = Animator.StringToHash("SpawnedPlayer");
        private IEventAggregator _eventAggregator;
        private Animator _stateMachine;

        public void Handle(PlayerSpawnedEvent @event)
        {
            _stateMachine.SetTrigger(SpawnedPlayer);
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _stateMachine = animator;
            _eventAggregator = EventAggregatorHolder.Instance;
            _eventAggregator.Subscribe(this);

            _eventAggregator.Publish(new SpawnPlayerEvent());
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _eventAggregator.Unsubscribe(this);
        }
    }
}