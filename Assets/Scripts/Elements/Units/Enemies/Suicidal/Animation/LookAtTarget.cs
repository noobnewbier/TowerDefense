using AgentAi.Suicidal.TargetProviders;
using UnityEngine;

namespace Elements.Units.Enemies.Suicidal.Animation
{
    public class LookAtTarget : MonoBehaviour
    {
        [Range(0, 5)] [SerializeField] private float speed;
        [SerializeField] private TargetProvider targetProvider;
        
        private void Update()
        {
            var targetPosition = targetProvider.Target.ObjectTransform.position;
            var selfTransform = transform;
            targetPosition.y = selfTransform.position.y;

            var targetRotation = Quaternion.LookRotation(
                targetPosition,
                selfTransform.up
            );

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                1 - Mathf.Exp(-speed * Time.deltaTime)
            );
        }
    }
}
