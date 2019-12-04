using System.Linq;
using Common.Class;
using Common.Event;
using Elements.Units.Enemies;
using EventManagement;
using UnityEngine;

namespace Statemachine
{
    public class ProgressState : StateMachineBehaviour, IHandle<PlayerDeadEvent>, IHandle<AllEnemyAgentsDeadEvent>
    {
        private static readonly int PlayerDies = Animator.StringToHash("PlayerDies");
        private static readonly int TurnFinishes = Animator.StringToHash("TurnFinishes");
        
        private IEventAggregator _eventAggregator;
        private Animator _animator;

        public void Handle(PlayerDeadEvent @event)
        {
            _animator.SetTrigger(PlayerDies);
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            _eventAggregator = EventAggregatorHolder.Instance;
            _eventAggregator.Subscribe(this);
            _animator = animator;

            _eventAggregator.Publish(new WaveStartEvent());
        }

        public void Handle(AllEnemyAgentsDeadEvent @event)
        {
            _animator.SetTrigger(TurnFinishes);
        }
    }
}