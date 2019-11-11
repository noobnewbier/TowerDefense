using System.Collections.Generic;
using System.Linq;
using Bullets;
using Bullets.InputSource;
using Common;
using Common.Events;
using EventManagement;
using Units.UnitCommon;
using UnityEngine;
using UnityUtils;

namespace Turrets
{
    public class Turret : BulletsShooterInputSource, IHandle<EnemyDeadEvent>
    {
        private const float UpdateTargetInterval = 0.5f;
        private IList<Unit> _enemiesInRange;
        private IEventAggregator _eventAggregator;
        private PooledMonoBehaviour _pooledBullet;
        private float _targetRefreshTimer;

        [SerializeField] private TurretData data;
        [SerializeField] private SphereCollider rangeCollider;
        [SerializeField] private Transform turretRotatable;

        public void Handle(EnemyDeadEvent @event)
        {
            _enemiesInRange.Remove(@event.Unit);
        }

        private void Awake()
        {
            _enemiesInRange = new List<Unit>();
            rangeCollider.radius = data.DetectionRange;
            rangeCollider.isTrigger = true;
        }

        private void OnEnable()
        {
            _eventAggregator = EventAggregatorHolder.Instance;
            _eventAggregator.Subscribe(this);
        }

        private void OnDisable()
        {
            _eventAggregator.Unsubscribe(this);
        }

        private void FixedUpdate()
        {
            Unit currentTarget = null;
            if (_targetRefreshTimer > UpdateTargetInterval)
            {
                currentTarget = data.TargetingStrategy.ChooseTarget(transform, _enemiesInRange);
                _targetRefreshTimer = 0f;
            }

            if (_enemiesInRange.Any() || FloatUtil.NearlyEqual(_targetRefreshTimer, 0f))
            {
                _targetRefreshTimer += Time.fixedDeltaTime;
            }

            var targetPosition = currentTarget != null ? currentTarget.Transform.position : (Vector3?) null;
            if (targetPosition.HasValue)
            {
                Aim(targetPosition.Value);
            }
        }

        private void Aim(Vector3 targetPosition)
        {
            var targetDir = Quaternion.LookRotation(targetPosition - turretRotatable.position);

            turretRotatable.rotation = Quaternion.RotateTowards(turretRotatable.rotation, targetDir, data.RotateSpeed * Time.fixedTime);
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

        //Shoot as long as we have enemies - Kill On Sight Comrade
        public override bool ReceivedShootBulletInput()
        {
            return _enemiesInRange.Any();
        }
    }
}