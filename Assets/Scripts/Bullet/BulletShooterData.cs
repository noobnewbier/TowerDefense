using UnityEngine;

namespace Bullet
{
    [CreateAssetMenu(menuName = "Data/BulletShooterData")]
    public class BulletShooterData : ScriptableObject
    {
        [field: SerializeField] public GameObject Bullet { get; }

        [field: SerializeField] public float ShootFrequency { get; }
    }
}