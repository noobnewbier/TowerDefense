using System;
using System.Collections;
using UnityEngine;
using UnityUtils;

namespace Elements.Turret.Animation
{
    public class TurretRecoil : MonoBehaviour
    {
        private Coroutine _currentCoroutine;
        private bool _isRecoiling;
        [SerializeField] private Transform originalTransform;
        [SerializeField] private float recoilEaseOutStrength = 2;
        [SerializeField] private Transform recoiledTransform;
        [SerializeField] private float recoilSpeed;
        [SerializeField] private float recoverySpeed;
        [SerializeField] private Timer shooterTimer;
        [SerializeField] private Timer animationTimer;
        

        private void OnEnable()
        {
            //required otherwise the timer remains un initialized, throwing an exception
            animationTimer.Init(recoilSpeed);
        }

        private void FixedUpdate()
        {
            //someone shooted
            if (shooterTimer.ResetInThisFrame)
            {
                Recoil();
            }
            else
            {
                //if it was recoiling, and the recoil movement finished 
                if (_isRecoiling && _currentCoroutine == null)
                {
                    Recover();
                }
            }
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
                        recoiledTransform.position,
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
        }
    }
}