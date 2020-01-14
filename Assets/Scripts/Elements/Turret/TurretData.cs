using System.Collections.Generic;
using Elements.Turret.TargetingPicking;
using Rules;
using UnityEngine;

namespace Elements.Turret
{
    [CreateAssetMenu(menuName = "Data/TurretData")]
    public class TurretData : ScriptableObject
    {
        [SerializeField] private int cost;
        [SerializeField] private float damageForUiDisplay;
        [SerializeField] private string description;
        [SerializeField] private float detectionRange;
        [SerializeField] private Fact[] facts;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private TargetingStrategy targetingStrategy;
        [SerializeField] private string turretName;

        public int Cost => cost;
        public string Description => description;
        public string TurretName => turretName;
        public float DamageForUiDisplay => damageForUiDisplay;
        public float RotateSpeed => rotateSpeed;
        public IEnumerable<Fact> Facts => facts;
        public float DetectionRange => detectionRange;
        public TargetingStrategy TargetingStrategy => targetingStrategy;
    }
}