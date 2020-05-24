using Common.Class;
using Common.Event;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Statemachine
{
    public class LoseState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            EventAggregatorHolder.Instance.Publish(new LostEvent());

            SceneManager.LoadScene("LoseScene");
        }
    }
}