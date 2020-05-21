using UnityEngine;
using UnityEngine.Serialization;

namespace Elements.Units.Enemies.Suicidal.Animation.InverseKinematics
{
    public abstract class IKSolver : MonoBehaviour
    {
        protected float[] angles;

        [FormerlySerializedAs("ikJoints")] [SerializeField]
        protected Joint[] joints;

        [SerializeField] protected Transform rootTransform;

        public abstract void InverseKinematics(Vector3 target);

        protected void RotateJoints()
        {
            for (var i = 0; i < joints.Length; i++)
            {
                var joint = joints[i];

                joint.transform.localRotation = Quaternion.AngleAxis(angles[i], joint.Axis);
            }
        }

        private void OnEnable()
        {
            angles = new float[joints.Length];
        }
    }
}