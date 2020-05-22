using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils;
using UnityUtils.Timers;

namespace Elements.Units.Enemies.Suicidal.Animation
{
    public class LegTargetController : MonoBehaviour
    {
        private float _initialHeight;
        private Vector3 _initialOffset;
        private float _initialOffsetMagnitude;
        private bool _isLerping;

        [Range(0, 10)] [SerializeField] private float farThresholdToMoveTargets;

        [FormerlySerializedAs("closeThresholdToMoveTargets")] [Range(0, 1)] [SerializeField]
        private float minimumProportionToInitialOffsetBeforeMoving;

        [Range(0f, 1f)] [SerializeField] private float overshootAmount;
        [Range(1, 10)] [SerializeField] private float smoothingFactor;
        [SerializeField] private ThresholdTimer timer;
        [SerializeField] private Transform unitTransform;

        [Range(0.05f, 5f)] [SerializeField] private float upliftAmount;


        private void OnEnable()
        {
            var position = transform.position;
            _initialOffset = position - unitTransform.position;
            _initialOffsetMagnitude = Vector3.Magnitude(_initialOffset);
            _initialHeight = position.y;
        }

        private void Update()
        {
            if (unitTransform == null) return;

            var selfPosition = transform.position;
            var unitPosition = unitTransform.position;
            var tooClose = Vector3.Distance(selfPosition, unitPosition) <
                           _initialOffsetMagnitude * minimumProportionToInitialOffsetBeforeMoving;
            var tooFar = Vector3.Distance(selfPosition, unitPosition + _initialOffset) >
                         farThresholdToMoveTargets;

            if ((tooFar || tooClose) && !_isLerping)
            {
                StopAllCoroutines();

                StartCoroutine(LerpingCoroutine());
            }
        }

        private IEnumerator LerpingCoroutine()
        {
            _isLerping = true;

            timer.Reset();

            yield return new WaitUntil(
                () =>
                {
                    if (unitTransform == null) return true; //break out of coroutine when unit is destroyed
                    
                    var originalPos = transform.position;
                    originalPos.y = _initialHeight;
                    var targetPos = _initialOffset + unitTransform.position;
                    targetPos.y = _initialHeight;

                    var directionVector = Vector3.Normalize(targetPos - originalPos);

                    var overshootTargetPos = targetPos + directionVector * overshootAmount;
                    var liftedMiddlePointPos =
                        (originalPos + overshootTargetPos) / 2f + unitTransform.up * upliftAmount;

                    var t = Easing.ExponentialEaseInOut(timer.NormalizedTime, smoothingFactor);
                    transform.position = Easing.QuadraticBezierLerping(
                        originalPos,
                        liftedMiddlePointPos,
                        overshootTargetPos,
                        t
                    );
                    var tryResetIfPassedThreshold = timer.TryResetIfPassedThreshold();
                    return tryResetIfPassedThreshold;
                }
            );

            _isLerping = false;
        }
    }
}