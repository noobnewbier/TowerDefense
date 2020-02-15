using System;
using System.Collections;
using UnityEngine;
using UnityUtils;
using Random = UnityEngine.Random;

namespace Elements.Turret.Animation
{
    public class TurretRecoil : MonoBehaviour
    {
        private Coroutine _currentCoroutine;
        private bool _isRecoiling;
        [SerializeField] private Timer animationTimer;
        [SerializeField] private bool isDebug;
        [SerializeField] private Transform originalTransform;
        [SerializeField] private float randomFactor;
        [SerializeField] private float recoilEaseOutStrength = 2;
        [SerializeField] private Transform recoiledTransform;
        [SerializeField] private float recoilSpeed;
        [SerializeField] private float recoverySpeed;
        [SerializeField] private Timer recoveryTimer;
        [SerializeField] private Timer shooterTimer;
        [SerializeField] private float timeTillRecovery;


        private void OnEnable()
        {
            //required otherwise the timer remains un initialized, throwing an exception
            animationTimer.Init(recoilSpeed);
            recoveryTimer.Init(timeTillRecovery);
        }

        private void Update()
        {
            if (isDebug) return;

            //someone shooted
            if (shooterTimer.ResetInThisFrame)
                Recoil();
            else if (_isRecoiling && recoveryTimer.TryResetIfPassedThreshold())
                //if it was recoiling, and the recoil movement finished 
                Recover();
        }

        [ContextMenu("Recoil")]
        private void Recoil()
        {
            if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);

            animationTimer.Init(recoilSpeed, true);
            _isRecoiling = true;

            _currentCoroutine =
                StartCoroutine(
                    AnimationCoroutine(
                        originalTransform.position,
                        recoiledTransform.position + Vector3.one * Random.Range(0, randomFactor),
                        Easing.EaseOutElastic
                    )
                );
        }

        [ContextMenu("Recovery")]
        private void Recover()
        {
            if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);

            animationTimer.Init(recoverySpeed, true);
            _isRecoiling = false;

            _currentCoroutine =
                StartCoroutine(
                    AnimationCoroutine(
                        recoiledTransform.position,
                        originalTransform.position,
                        f => Easing.ExponentialEaseOut(f, recoilEaseOutStrength)
                    )
                );
        }

        private IEnumerator AnimationCoroutine(Vector3 originalPosition,
                                               Vector3 targetPosition,
                                               Func<float, float> easingFunction)
        {
            yield return new WaitUntil(
                () =>
                {
                    transform.position = Vector3.Lerp(
                        originalPosition,
                        targetPosition,
                        easingFunction(animationTimer.NormalizedTime)
                    );
                    return FloatUtil.NearlyEqual(animationTimer.NormalizedTime, 1);
                }
            );
            _currentCoroutine = null;
            recoveryTimer.Reset();
        }
    }
}