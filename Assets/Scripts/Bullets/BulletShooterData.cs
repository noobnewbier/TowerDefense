using UnityEngine;

namespace Bullets
{
    [CreateAssetMenu(fileName = "Data/BulletShooterData")]
    public class BulletShooterData : ScriptableObject
    {
        [SerializeField] private GameObject bullet;
        [SerializeField] private float shootFrequency;

        public GameObject Bullet => bullet;
        public float ShootFrequency => shootFrequency;
    }
}