using UnityEngine;
using UnityUtils;
using UnityUtils.Timers;

namespace Elements.Units.Enemies.Suicidal.Animation
{
    public class HoppingAnimation : MonoBehaviour
    {
        private float _initialHeight;
        [SerializeField] private Transform animatedTransform;

        [Range(1f, 10f)] [SerializeField] private float easingFactor;

        //unnecessarily bloated...
        [SerializeField] private HasForwardInput hasForwardInput;
        [SerializeField] private float jumpMagnitude;

        [SerializeField] private BouncingTimer timer;


        private void OnEnable()
        {
            _initialHeight = animatedTransform.position.y;
        }

        private void Update()
        {
            if (hasForwardInput.ProvideBoolean() ||
                !FloatUtil.NearlyEqual(animatedTransform.position.y, _initialHeight))
            {
                var t = Mathf.Abs(Mathf.Sin(timer.NormalizedTime * easingFactor));
                var smoothedY = Mathf.Lerp(_initialHeight, _initialHeight + jumpMagnitude, t);

                var position = animatedTransform.position;
                position = new Vector3(position.x, smoothedY, position.z);
                animatedTransform.position = position;
            }
            else
            {
                timer.Reset();
            }
        }
    }
}