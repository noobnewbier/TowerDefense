using System;
using System.Collections.Generic;
using Bullets;
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
        [SerializeField] private SphereCollider rangeCollider;

        private IList<IEnemy> _enemiesInRange;

        private void Awake()
        {
            _enemiesInRange = new List<IEnemy>();

            rangeCollider.radius = data.DetectionRange;
            rangeCollider.isTrigger = true;
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
            var targetDir = targetPosition - turretRotatable.position;
            Vector3.RotateTowards(turretRotatable.forward, targetDir, data.RotateSpeed * Time.fixedDeltaTime, 0f);
        }
        
        private void Shoot()
        {
            Instantiate(data.Bullet, bulletSpawnPoint);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(ObjectTags.Enemy))
            {
                _enemiesInRange.Add(other.gameObject.GetComponent<IEnemy>());  
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            _enemiesInRange.Remove(other.GetComponent<IEnemy>());
        }
    }
}