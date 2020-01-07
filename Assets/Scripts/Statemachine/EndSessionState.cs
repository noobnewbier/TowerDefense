using UnityEngine;

namespace Statemachine
{
    // Used by AI training
    public class EndSessionState : StateMachineBehaviour
    {
        private static readonly int WrappedUpSession = Animator.StringToHash("WrappedUpSession");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetTrigger(WrappedUpSession);
        }
    }
}