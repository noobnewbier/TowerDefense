using Common.Event;
using EventManagement;
using EventManagement.Providers;
using Experimental;
using UnityEngine;

namespace Statemachine
{
    // Used by AI training
    public class EndSessionState : StateMachineBehaviour
    {
        private static readonly int WrappedUpSession = Animator.StringToHash("WrappedUpSession");
        [SerializeField] private EventAggregatorProvider eventAggregatorProvider;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            eventAggregatorProvider.ProvideEventAggregator().Publish(new WaveEndEvent());
            animator.SetTrigger(WrappedUpSession);
        }
    }
}