using System;
using System.Collections;
using UnityEngine;
using UnityUtils;
using UnityUtils.BooleanProviders;
using UnityUtils.Timers;
using Random = UnityEngine.Random;

namespace Elements.Turret.Animation
{
    public class TurretRecoil : MonoBehaviour
    {
        private Coroutine _currentCoroutine;
        private bool _isRecoiling;
        [SerializeField] private ThresholdTimer animationTimer;
        [Range(0, 1f)] [SerializeField] private float currentPositionStrength;
        [SerializeField] private bool isDebug;
        [SerializeField] private StateRepresenter isShootingState;
        [SerializeField] private Transform originalTransform;
        [SerializeField] private float randomFactor;
        [SerializeField] private float recoilEaseOutStrength = 2;
        [SerializeField] private Transform recoiledTransform;
        [SerializeField] private float recoilSpeed;
        [SerializeField] private float recoverySpeed;
        [SerializeField] private ThresholdTimer recoveryTimer;
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
            if (isShootingState.ProvideBoolean())
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

        private IEnumerator AnimationCoroutine(Vector3 startingPosition,
                                               Vector3 targetPosition,
                                               Func<float, float> easingFunction)
        {
            var currentPosition = transform.position;
            yield return new WaitUntil(
                () =>
                {
                    transform.position = Vector3.Lerp(
                        startingPosition * (1f - currentPositionStrength) + currentPosition * currentPositionStrength,
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