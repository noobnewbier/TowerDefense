using System.Diagnostics;
using Common.Constant;
using Common.Event;
using EventManagement.Providers;
using ScriptableService;
using UnityEngine;

namespace Statemachine
{
    // Used by AI training
    public class EndSessionState : StateMachineBehaviour
    {
        private static readonly int WrappedUpSession = Animator.StringToHash("WrappedUpSession");
        private static readonly int EndGame = Animator.StringToHash("EndGame");
        private static readonly int WaveEnds = Animator.StringToHash("WaveEnds");
        [SerializeField] private EventAggregatorProvider eventAggregatorProvider;
        [SerializeField] private PlayerInstanceTracker playerInstanceTracker;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            eventAggregatorProvider.ProvideEventAggregator().Publish(new WaveEndEvent());

            WrapUpTraining(animator);
            EndWave(animator);
        }

        [Conditional(GameConfig.TrainingMode)]
        private void WrapUpTraining(Animator animator)
        {
            animator.SetTrigger(WrappedUpSession);
        }

        [Conditional(GameConfig.GameplayMode)]
        private void EndWave(Animator animator)
        {
            animator.SetTrigger(playerInstanceTracker.Player == null ? EndGame : WaveEnds);
        }
    }
}