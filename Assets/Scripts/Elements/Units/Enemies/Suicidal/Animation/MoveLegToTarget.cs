using Elements.Units.Enemies.Suicidal.Animation.InverseKinematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Elements.Units.Enemies.Suicidal.Animation
{
    public class MoveLegToTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [FormerlySerializedAs("ikSolver")] [SerializeField] private IKSolver gradientDescentIKSolver;

        private void Update()
        {
            gradientDescentIKSolver.InverseKinematics(target.transform.position);
        }
    }
}