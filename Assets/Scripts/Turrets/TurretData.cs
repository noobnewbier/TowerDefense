using Bullets;
using Turrets.TargetingStrategies;
using UnityEngine;
// ReSharper disable ConvertToAutoProperty

namespace Turrets
{
    [CreateAssetMenu(menuName = "Data/TurretData")]
    public class TurretData : ScriptableObject
    {
        [SerializeField] private float rotateSpeed;
        public float RotateSpeed => rotateSpeed;
        
        [SerializeField] private GameObject bullet;
        public GameObject Bullet => bullet;

        [SerializeField] private float detectionRange;
        public float DetectionRange => detectionRange;

        [SerializeField] private TargetingStrategy targetingStrategy;
        public TargetingStrategy TargetingStrategy => targetingStrategy;
    }
}