using Elements.Units.Enemies.Suicidal.Animation.InverseKinematics;
using UnityEngine;

namespace Elements.Units.Enemies.Suicidal.Animation
{
    public class MoveLegToTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private IKSolver ikSolver;

        private void Update()
        {
            ikSolver.InverseKinematics(target.transform.position);
        }
    }
}