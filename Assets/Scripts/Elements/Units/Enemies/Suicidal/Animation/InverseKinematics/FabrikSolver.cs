using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elements.Units.Enemies.Suicidal.Animation.InverseKinematics
{
    public class FabrikSolver : IKSolver
    {
        private Vector3[] Forward(IReadOnlyList<Vector3> positions, Vector3 startingPosition)
        {
            var currentTarget = startingPosition;
            var newPositions = new Vector3[positions.Count];
            for (var i = 0; i < positions.Count - 1; i++)
            {
                var newPositionForPreviousJoint = GetJointAPosition(positions[i + 1], positions[i], currentTarget);
                newPositions[i] = currentTarget;
                newPositions[i + 1] = newPositionForPreviousJoint;

                currentTarget = newPositionForPreviousJoint;
            }

            return newPositions;
        }

        private Vector3[] Backward(IReadOnlyList<Vector3> positions, Vector3 target)
        {
            var currentTarget = target;
            var newPositions = new Vector3[positions.Count];
            for (var i = positions.Count; i-- > 1;)
            {
                var newPositionForPreviousJoint = GetJointAPosition(positions[i - 1], positions[i], currentTarget);
                newPositions[i] = currentTarget;
                newPositions[i - 1] = newPositionForPreviousJoint;

                currentTarget = newPositionForPreviousJoint;
            }

            return newPositions;
        }

        private float[] GetAnglesToMeetPosition(Vector3[] positions)
        {
            for (var i = 0; i < joints.Length - 1; i++)
            {
                var jointTransform = joints[i].transform;
                var worldRotation = Quaternion.LookRotation(positions[i + 1] - positions[i], jointTransform.up);
                var localRotation = Quaternion.Inverse(jointTransform.parent.rotation) * worldRotation;
                //BUG: the axis might be a mix of XYZ, but this has to do for now until I figure out the maths
                angles[i] = localRotation.eulerAngles.magnitude;
            }

            return angles;
        }

        public override void InverseKinematics(Vector3 target)
        {
            var currentPositions = joints.Select(j => j.transform.position).ToArray();
            currentPositions = Backward(currentPositions, target);
            currentPositions = Forward(currentPositions, joints.First().transform.position);
            angles = GetAnglesToMeetPosition(currentPositions);
            
            RotateJoints();
        }

        //Doesn't matter if it is A---B or B--A, always change A's position to meet the requirement of target position for B
        private Vector3 GetJointAPosition(Vector3 a, Vector3 b, Vector3 targetB)
        {
            var direction = Vector3.Normalize(targetB - a);
            var distance = Vector3.Distance(a, b);

            return targetB + direction * distance;
        }
    }
}