using System.Collections.Generic;
using UnityEngine;

namespace Elements.Units.Enemies.Suicidal.Animation.InverseKinematics
{
    //ref: https://www.alanzucconi.com/2017/04/10/robotic-arms/
    public class IKSolver : MonoBehaviour
    {
        private float[] _angles;
        [SerializeField] private float distanceThreshold;
        [SerializeField] private Joint[] joints;
        [SerializeField] private float learningRate;
        [SerializeField] private float samplingDistance;
        [SerializeField] private float undergroundPenalty;

        private void OnEnable()
        {
            _angles = new float[joints.Length];
        }

        private Vector3 ForwardKinematics(IReadOnlyList<float> angles)
        {
            var prevPoint = joints[0].transform.position;
            var rotation = Quaternion.identity;
            for (var i = 1; i < joints.Length; i++)
            {
                // Rotates around a new axis
                rotation *= Quaternion.AngleAxis(angles[i - 1], joints[i - 1].Axis);
                var nextPoint = prevPoint + rotation * joints[i].StartOffset;

                prevPoint = nextPoint;
            }

            return prevPoint;
        }

        public void InverseKinematics(Vector3 target)
        {
            if (CostFromTarget(target, _angles) < distanceThreshold)
                return;

            for (var i = 0; i < joints.Length; i++)
            {
                // Gradient descent
                // Update : Solution -= LearningRate * Gradient
                var gradient = PartialGradient(target, _angles, i);
                var newAngle = _angles[i] - learningRate * gradient;
                newAngle = Mathf.Clamp(newAngle, joints[i].MinAngle, joints[i].MaxAngle);
                newAngle = float.IsNaN(newAngle) ? _angles[i] : newAngle;

                _angles[i] = newAngle;

                // Early termination, todo: do we really need this?
                if (CostFromTarget(target, _angles) < distanceThreshold)
                    return;
            }

            RotateJoints();
        }

        private void RotateJoints()
        {
            for (var i = 0; i < joints.Length; i++)
            {
                var joint = joints[i];

                joint.transform.localRotation = Quaternion.AngleAxis(_angles[i], joint.Axis);
            }
        }

        private float PartialGradient(Vector3 target, float[] angles, int i)
        {
            // Saves the angle,
            // it will be restored later
            var angle = angles[i];

            // Gradient : [F(x+SamplingDistance) - F(x)] / h
            var fX = CostFromTarget(target, angles);

            angles[i] += samplingDistance;
            var fXPlusD = CostFromTarget(target, angles);

            var gradient = (fXPlusD - fX) / samplingDistance;

            // Restores
            angles[i] = angle;

            Debug.Log(gradient);
            return gradient;
        }

        private float CostFromTarget(Vector3 target, IReadOnlyList<float> angles)
        {
            var point = ForwardKinematics(angles);
            var distance = Vector3.Distance(point, target);
            var isUnderGroundPenalty = Mathf.Max(0, -point.y) * undergroundPenalty;
            return distance + isUnderGroundPenalty;
        }
    }
}