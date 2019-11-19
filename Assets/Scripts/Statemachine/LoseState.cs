using Common;
using Common.Class;
using Common.Events;
using UnityEngine;

namespace StateMachine
{
    public class LoseState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            EventAggregatorHolder.Instance.Publish(new LostEvent());
        }
    }
}