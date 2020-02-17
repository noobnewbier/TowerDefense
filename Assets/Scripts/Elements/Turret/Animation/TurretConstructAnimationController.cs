using System.Collections;
using UnityEngine;
using UnityUtils;
using UnityUtils.Timers;

namespace Elements.Turret.Animation
{
    public class TurretConstructAnimationController : MonoBehaviour
    {
        [SerializeField] private ThresholdTimer animationTimer;
        [SerializeField] private DissolveShaderController[] shaderControllers;

        [ContextMenu("StartTurretSpawnAnimation")]
        public void StartTurretSpawnAnimation()
        {
            StartCoroutine(TurretSpawnAnimationCoroutine());
        }

        [ContextMenu("StopTurretSpawnAnimation")]
        public void StopTurretSpawnAnimation()
        {
            StartCoroutine(TurretDestructAnimationCoroutine());
        }

        private IEnumerator TurretSpawnAnimationCoroutine()
        {
            animationTimer.Reset();
            yield return new WaitUntil(() =>
            {
                AnimateDissolveShaders(Easing.Flip(animationTimer.NormalizedTime));

                return animationTimer.PassedThreshold;
            });
        }

        private void AnimateDissolveShaders(float easedTime)
        {
            foreach (var controller in shaderControllers)
            {
                controller.SetPropertyBlockDissolveAmount(easedTime);
            }
        }

        private IEnumerator TurretDestructAnimationCoroutine()
        {
            animationTimer.Reset();
            yield return new WaitUntil(() =>
            {
                AnimateDissolveShaders(animationTimer.NormalizedTime);

                return animationTimer.PassedThreshold;
            });
        }
    }
}