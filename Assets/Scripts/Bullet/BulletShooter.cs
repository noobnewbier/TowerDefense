using Bullet.InputSource;
using UnityEngine;
using UnityUtils;

namespace Bullet
{
    public class BulletShooter : MonoBehaviour
    {
        private PooledMonoBehaviour _pooledBullet;
        private float _timer;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private BulletShooterData data;
        [SerializeField] private BulletsShooterInputSource inputSource;

        private void Awake()
        {
            _pooledBullet = data.Bullet.GetComponent<PooledMonoBehaviour>();
        }

        private void Shoot()
        {
            var newBullet = _pooledBullet.GetPooledInstance();

            newBullet.transform.position = bulletSpawnPoint.position;
            newBullet.transform.rotation = bulletSpawnPoint.rotation;
        }

        private void FixedUpdate()
        {
            if (inputSource.ReceivedShootBulletInput() && _timer >= data.ShootFrequency)
            {
                _timer = 0f;
                Shoot();
            }
            else
            {
                _timer += Time.deltaTime;
            }
        }
    }
}