using Bullet.InputSource;
using Elements.Turret;
using UnityEngine;
using UnityUtils;

namespace Bullet
{
    public class BulletShooter : MonoBehaviour
    {
        private PooledMonoBehaviour _pooledBullet;
        private IBulletShooterRepository _repository;

        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private BulletsShooterInputSource inputSource;
        [SerializeField] private BulletShooterRepositoryProvider provider;
        [SerializeField] private Timer timer;
        [Range(0, 1)] [SerializeField] private float randomFactor = 0.85f;


        private void OnEnable()
        {
            _repository = provider.ProvideRepository();

            _pooledBullet = _repository.PooledBullet;
            timer.Init(_repository.ShootFrequency);
        }

        private void Shoot()
        {
            var newBullet = _pooledBullet.GetPooledInstance();
            newBullet.transform.position = bulletSpawnPoint.position;

            //introduce slight inconsistency in the shooting projectile
            var rand = Random.Range(randomFactor, 1f);
            var parallelToGroundRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
            var bulletRotation = Quaternion.Lerp(bulletSpawnPoint.rotation, parallelToGroundRotation, rand);
            newBullet.transform.rotation = bulletRotation;
        }

        private void FixedUpdate()
        {
            if (inputSource.ReceivedShootBulletInput() && timer.TryResetIfPassedThreshold()) Shoot();
        }
    }
}