using Bullet;
using UnityUtils;

namespace Elements.Turret
{
    public interface IBulletShooterRepository
    {
        PooledMonoBehaviour PooledBullet { get; }
        float ShootFrequency { get; }
    }

    public class BulletShooterRepository : IBulletShooterRepository
    {
        private readonly BulletShooterData _bulletShooterData;

        public BulletShooterRepository(BulletShooterData bulletShooterData)
        {
            _bulletShooterData = bulletShooterData;
        }

        public PooledMonoBehaviour PooledBullet => _bulletShooterData.Bullet.GetComponent<PooledMonoBehaviour>();
        public float ShootFrequency => _bulletShooterData.ShootFrequency;
    }
}