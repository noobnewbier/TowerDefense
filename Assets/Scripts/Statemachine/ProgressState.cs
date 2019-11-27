using System.Linq;
using Common.Class;
using Common.Event;
using Elements.Units.Enemies;
using EventManagement;
using UnityEngine;

namespace Statemachine
{
    public class ProgressState : StateMachineBehaviour, IHandle<PlayerDeadEvent>, IHandle<EnemyDeadEvent>
    {
        private static readonly int PlayerDies = Animator.StringToHash("PlayerDies");
        private static readonly int TurnFinishes = Animator.StringToHash("TurnFinishes");

        private Animator _animator;
        private IEventAggregator _eventAggregator;
        private int _killedCount;
        private int _totalSpawnerCount;

        public void Handle(EnemyDeadEvent @event)
        {
            _killedCount++;
            if (_killedCount >= _totalSpawnerCount) _animator.SetTrigger(TurnFinishes);
        }

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
            _totalSpawnerCount = FindObjectsOfType<EnemySpawner>().Sum(e => e.TotalEnemyCount);
            _killedCount = 0;

            _eventAggregator.Publish(new WaveStartEvent());
        }
    }
}