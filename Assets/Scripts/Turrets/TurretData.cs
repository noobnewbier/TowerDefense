using Turrets.TargetingStrategies;
using UnityEngine;

// ReSharper disable ConvertToAutoProperty

namespace Turrets
{
    [CreateAssetMenu(menuName = "Data/TurretData")]
    public class TurretData : ScriptableObject
    {
        [SerializeField] private float detectionRange;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private TargetingStrategy targetingStrategy;
        public float RotateSpeed => rotateSpeed;
        public float DetectionRange => detectionRange;
        public TargetingStrategy TargetingStrategy => targetingStrategy;
    }
}