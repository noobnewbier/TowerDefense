using System;
using System.Collections.Generic;
using Common;
using Enemies;
using UnityEngine;

namespace Turrets
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretData data;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private Transform turretRotatable;
        [SerializeField] private Collider rangeCollider;

        private IList<IEnemy> _enemiesInRange;

        private void Awake()
        {
            _enemiesInRange = new List<IEnemy>();
        }

        private void FixedUpdate()
        {
            var targetPosition = data.TargetingStrategy.ChooseTarget(transform, _enemiesInRange);

            if (targetPosition.HasValue)
            {
                Aim(targetPosition.Value);
                Shoot();
            }
        }

        private void Aim(Vector3 targetPosition)
        {
            var selfTransform = transform;
            var targetDir = targetPosition - selfTransform.position;
            Vector3.RotateTowards(selfTransform.forward, targetDir, data.RotateSpeed * Time.fixedDeltaTime, 0f);
        }
        
        private void Shoot()
        {
            Instantiate(data.Bullet, bulletSpawnPoint);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag(ObjectTags.Enemy))
            {
                _enemiesInRange.Add(other.gameObject.GetComponent<IEnemy>());  
            }
        }
    }
}