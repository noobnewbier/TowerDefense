using UnityEngine;

namespace Elements.Units.Enemies.Suicidal.Animation.InverseKinematics
{
    public class Joint : MonoBehaviour
    {
        [SerializeField] private Vector3 axis;
        [SerializeField] private float maxAngle;
        [SerializeField] private float minAngle;
        [SerializeField] private Joint previousJoint;

        public float MinAngle => minAngle;
        public float MaxAngle => maxAngle;
        public Vector3 Axis => axis;
        public Vector3 StartOffset { get; private set; }

        private void OnEnable()
        {
            if (previousJoint != null)
                StartOffset = transform.position - previousJoint.transform.position;
            else // is initial joint
                StartOffset = Vector3.zero;
        }

        private void OnValidate()
        {
            if (minAngle > maxAngle)
            {
                Debug.Log(gameObject.name + "'s Joint could not have minAngle greater than maxAngle");
                minAngle = maxAngle;
            }
        }
    }
}