using System.Collections.Generic;
using Elements.Turret.TargetingPicking;
using Rules;
using UnityEngine;

namespace Elements.Turret
{
    [CreateAssetMenu(menuName = "Data/TurretData")]
    public class TurretData : ScriptableObject
    {
        [SerializeField] private float detectionRange;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private TargetingStrategy targetingStrategy;
        [SerializeField] private Fact[] facts;

        public float RotateSpeed => rotateSpeed;
        public IEnumerable<Fact> Facts => facts;
        public float DetectionRange => detectionRange;
        public TargetingStrategy TargetingStrategy => targetingStrategy;
    }
}