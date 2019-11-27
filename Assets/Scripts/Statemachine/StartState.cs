using Common.Class;
using Common.Event;
using UnityEngine;

namespace Statemachine
{
    public class StartState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            EventAggregatorHolder.Instance.Publish(new GameStartEvent());
        }
    }
}