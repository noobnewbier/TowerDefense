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
        
        [SerializeField] private Bullet bullet;
        public Bullet Bullet => bullet;

        [SerializeField] private ITargetingStrategy targetingStrategy;
        public ITargetingStrategy TargetingStrategy => targetingStrategy;
    }
}