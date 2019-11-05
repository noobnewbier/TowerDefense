using System.Collections.Generic;
using System.Linq;
using Bullets;
using Common;
using Units.UnitCommon;
using UnityEngine;
using UnityUtils;

namespace Turrets
{
    public class Turret : MonoBehaviour
    {
        private const float UpdateTargetInterval = 0.5f;

        private Unit _currentTarget;

        private IList<Unit> _enemiesInRange;
        private PooledMonoBehaviour _pooledBullet;
        private float _targetRefreshTimer;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private TurretData data;
        [SerializeField] private SphereCollider rangeCollider;
        [SerializeField] private Transform turretRotatable;

        private void Awake()
        {
            _pooledBullet = data.Bullet.GetComponent<PooledMonoBehaviour>();

            _enemiesInRange = new List<Unit>();
            rangeCollider.radius = data.DetectionRange;
            rangeCollider.isTrigger = true;
        }

        private void FixedUpdate()
        {
            if (_targetRefreshTimer > UpdateTargetInterval)
            {
                _currentTarget = data.TargetingStrategy.ChooseTarget(transform, _enemiesInRange);
                _targetRefreshTimer = 0f;
            }

            if (_enemiesInRange.Any() || FloatUtil.NearlyEqual(_targetRefreshTimer, 0f))
            {
                _targetRefreshTimer += Time.fixedDeltaTime;
            }

            var targetPosition = _currentTarget != null ? _currentTarget.Transform.position : (Vector3?) null;
            if (targetPosition.HasValue)
            {
                Aim(targetPosition.Value);
                Shoot();
            }
        }

        private void Aim(Vector3 targetPosition)
        {
            var targetDir = Quaternion.LookRotation(targetPosition - turretRotatable.position);

            turretRotatable.rotation = Quaternion.RotateTowards(turretRotatable.rotation, targetDir, data.RotateSpeed * Time.fixedTime);
        }

        private void Shoot()
        {
            var newBullet = _pooledBullet.GetPooledInstance();

            newBullet.transform.position = bulletSpawnPoint.position;
            newBullet.transform.rotation = bulletSpawnPoint.rotation;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(ObjectTags.Enemy))
            {
                _enemiesInRange.Add(other.gameObject.GetComponent<Unit>());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(ObjectTags.Enemy))
            {
                _enemiesInRange.Remove(other.GetComponent<Unit>());
            }
        }
    }
}