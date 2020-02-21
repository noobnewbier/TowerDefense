using UnityEngine;

namespace Elements.Turret.Rotation
{
    public class DirectRotationRotator : Rotator
    {
        [SerializeField] private Transform controlledTransform;

        public override void LookAt(Vector3 target, float speed)
        {
            var targetDir = Quaternion.LookRotation(target - controlledTransform.position);

            controlledTransform.rotation = Quaternion.RotateTowards(
                controlledTransform.rotation,
                targetDir,
                speed * Time.fixedDeltaTime
            );
        }
    }
}