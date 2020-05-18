using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Elements.Units.Enemies.Suicidal.Animation.InverseKinematics
{
    //ref: https://www.alanzucconi.com/2017/04/10/robotic-arms/
    public class IKSolver : MonoBehaviour
    {
        private float[] _angles;
        [SerializeField] private IKConfig config;

        [SerializeField] private FKCache fkCache;

        [FormerlySerializedAs("ikJoints")] [SerializeField]
        private Joint[] joints;

        [SerializeField] private Transform rootTransform;


        private void OnEnable()
        {
            _angles = new float[joints.Length];
        }

        private void Start()
        {
            fkCache.CreateCache(rootTransform, config, joints);
        }

        public void InverseKinematics(Vector3 target)
        {
            if (CostFromTarget(target, _angles) < config.DistanceThreshold)
                return;

            for (var i = 0; i < joints.Length; i++)
            {
                // Gradient descent
                // Update : Solution -= LearningRate * Gradient
                var gradient = PartialGradient(target, _angles, i);
                var newAngle = _angles[i] - config.LearningRate * gradient;
                newAngle = Mathf.Clamp(newAngle, joints[i].MinAngle, joints[i].MaxAngle);
                newAngle = float.IsNaN(newAngle) ? _angles[i] : newAngle;

                _angles[i] = newAngle;

                // Early termination, todo: do we really need this?
                if (CostFromTarget(target, _angles) < config.DistanceThreshold)
                {
                    return;
                }
                
                Debug.Log(CostFromTarget(target, _angles));
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

            angles[i] += config.SamplingDistance;
            var fXPlusD = CostFromTarget(target, angles);

            var gradient = (fXPlusD - fX) / config.SamplingDistance;

            // Restores
            angles[i] = angle;

            return gradient;
        }

        private float CostFromTarget(Vector3 target, IReadOnlyList<float> angles)
        {
            var point = IKUtils.ForwardKinematics(angles, joints);
            var distance = Vector3.Distance(point, target);
            var isUnderGroundPenalty = Mathf.Max(0, -point.y) * config.UndergroundPenalty;
            return distance + isUnderGroundPenalty;
        }
    }
}