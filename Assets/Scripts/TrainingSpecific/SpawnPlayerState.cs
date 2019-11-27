using Common.Class;
using UnityEngine;

namespace TrainingSpecific
{
    public class SpawnPlayerState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            EventAggregatorHolder.Instance.Publish(new SpawnPlayerEvent());
        }
    }
}