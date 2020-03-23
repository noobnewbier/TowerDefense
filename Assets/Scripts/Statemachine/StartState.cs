using Common.Class;
using Common.Event;
using UnityEngine;

namespace Statemachine
{
    public class StartState : StateMachineBehaviour
    {
        //for some reason, when running in a very high speed, onStateEnter is not called in THIS fucking state 
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            EventAggregatorHolder.Instance.Publish(new GameStartEvent());
        }
    }
}