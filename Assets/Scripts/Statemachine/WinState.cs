using Common;
using Common.Events;
using EventManagement;
using UnityEngine;

namespace StateMachine
{
    public class WinState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            
            EventAggregatorHolder.Instance.Publish(new WinEvent());
        }
    }
}