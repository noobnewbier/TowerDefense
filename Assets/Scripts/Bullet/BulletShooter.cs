using Bullet.InputSource;
using Elements.Turret;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils;
using UnityUtils.BooleanProviders;
using UnityUtils.Timers;

namespace Bullet
{
    public class BulletShooter : MonoBehaviour
    {
        private PooledMonoBehaviour _pooledBullet;
        private IBulletShooterRepository _repository;

        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private BulletsShooterInputSource inputSource;

        [Tooltip("Most random when 0, least random at 1")]
        [FormerlySerializedAs("randomFactor")]
        [Range(0, 1)]
        [SerializeField]
        private float inverseRandomFactor = 0.85f;

        [SerializeField] private StateRepresenter isShootingState;

        [Range(0, 1)] [SerializeField] private float parallelToGroundStrength = 0;

        [SerializeField] private BulletShooterRepositoryProvider provider;
        [SerializeField] private ThresholdTimer timer;


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
            var bulletSpawnPointRotation = bulletSpawnPoint.rotation;
            var spawnpointEuler = bulletSpawnPointRotation.eulerAngles;
            //introduce slight inconsistency in the shooting projectile
            var rand = Random.Range(inverseRandomFactor, 1f);
            var parallelToGroundRotation = Quaternion.Euler(0f, spawnpointEuler.y, spawnpointEuler.z);
            var bulletRotation = Quaternion.Lerp(bulletSpawnPointRotation, parallelToGroundRotation, rand * parallelToGroundStrength);
            newBullet.transform.rotation = bulletRotation;
        }

        private void FixedUpdate()
        {
            if (inputSource.ReceivedShootBulletInput() && timer.TryResetIfPassedThreshold())
            {
                Shoot();
                isShootingState.SetState(true);
            }
            else
            {
                isShootingState.SetState(false);
            }
        }
    }
}