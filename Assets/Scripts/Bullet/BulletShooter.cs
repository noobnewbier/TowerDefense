using Bullet.InputSource;
using Elements.Turret;
using UnityEngine;
using UnityUtils;

namespace Bullet
{
    public class BulletShooter : MonoBehaviour
    {
        private PooledMonoBehaviour _pooledBullet;
        private float _timer;
        private IBulletShooterRepository _repository;

        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private BulletsShooterInputSource inputSource;
        [SerializeField] private BulletShooterRepositoryProvider provider;

        private void OnEnable()
        {
            _repository = provider.ProvideRepository();

            _pooledBullet = _repository.PooledBullet;
        }

        private void Shoot()
        {
            var newBullet = _pooledBullet.GetPooledInstance();

            newBullet.transform.position = bulletSpawnPoint.position;
            newBullet.transform.rotation = bulletSpawnPoint.rotation;
        }

        private void FixedUpdate()
        {
            if (inputSource.ReceivedShootBulletInput() && _timer >= _repository.ShootFrequency)
            {
                _timer = 0f;
                Shoot();
            }
            else
                _timer += Time.deltaTime;
        }
    }
}