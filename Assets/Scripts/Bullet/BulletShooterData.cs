using UnityEngine;

namespace Bullet
{
    [CreateAssetMenu(menuName = "Data/BulletShooterData")]
    public class BulletShooterData : ScriptableObject
    {
        [SerializeField] private GameObject bullet;
        [SerializeField] private float shootFrequency;

        public GameObject Bullet => bullet;
        public float ShootFrequency => shootFrequency;
    }
}