using System.Collections.Generic;
using UnityEngine;

namespace Elements.Units.Enemies.Suicidal.Animation.InverseKinematics
{
    public static class IKUtils
    {
        public static Vector3 ForwardKinematics(IReadOnlyList<float> angles, IReadOnlyList<Joint> joints)
        {
            var prevPoint = joints[0].transform.position;
            var rotation = Quaternion.identity;
            for (var i = 1; i < joints.Count; i++)//todo: revert back to 0 start
            {
                // Rotates around a new axis
                rotation *= Quaternion.AngleAxis(angles[i - 1], joints[i - 1].Axis);
                var nextPoint = prevPoint + rotation * joints[i].StartOffset;

                prevPoint = nextPoint;
            }

            return prevPoint;
        }       
    }
}