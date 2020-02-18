using System.Collections;
using UnityEngine;
using UnityUtils;
using UnityUtils.Timers;

namespace Elements.Turret.Animation
{
    public interface IHasAnimation
    {
        bool IsPlayingAnimation { get; }
    }
    public interface IConstructAnimationController : IHasAnimation
    {
        void ConstructTurretAnimation();
    }

    public interface IDestructAnimationController : IHasAnimation
    {
        void DestructTurretAnimation();
    }

    public class TurretAnimationController : MonoBehaviour, IConstructAnimationController, IDestructAnimationController
    {
        [SerializeField] private ThresholdTimer animationTimer;
        [SerializeField] private DissolveShaderController[] shaderControllers;

        private Coroutine _currentCoroutine;
        
        [ContextMenu("ConstructTurretAnimation")]
        public void ConstructTurretAnimation()
        {
            _currentCoroutine = StartCoroutine(ConstructTurretAnimationCoroutine());
        }

        [ContextMenu("DestructTurretAnimation")]
        public void DestructTurretAnimation()
        {
            _currentCoroutine = StartCoroutine(TurretDestructAnimationCoroutine());
        }

        private IEnumerator ConstructTurretAnimationCoroutine()
        {
            animationTimer.Reset();
            yield return new WaitUntil(() =>
            {
                AnimateDissolveShaders(Easing.Flip(animationTimer.NormalizedTime));

                return animationTimer.PassedThreshold;
            });
            
            _currentCoroutine = null;
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
            
            _currentCoroutine = null;
        }

        public bool IsPlayingAnimation => _currentCoroutine != null;
    }
}