using System;
using System.Collections.Generic;
using System.Linq;
using Bullet.InputSource;
using Common.Class;
using Common.Enum;
using Common.Interface;
using EventManagement;
using Units.UnitCommon;
using UnityEngine;
using UnityUtils;

namespace Turret
{
    [RequireComponent(typeof(UnitDetector))]
    public class Turret : BulletsShooterInputSource, IObjectOfInterest
    {
        private const float UpdateTargetInterval = 0.5f;

        private Unit _currentTarget;

        private IEventAggregator _eventAggregator;
        private PooledMonoBehaviour _pooledBullet;
        private float _targetRefreshTimer;

        [SerializeField] private TurretData data;
        [SerializeField] private Transform turretRotatable;
        [SerializeField] private UnitDetector unitDetector;

        private void Awake()
        {
            unitDetector.Initialize(data);
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
            if (_targetRefreshTimer > UpdateTargetInterval)
            {
                _currentTarget = data.TargetingStrategy.ChooseTarget(transform, unitDetector.EnemiesInRange);
                _targetRefreshTimer = 0f;
            }

            if (unitDetector.EnemiesInRange.Any() || FloatUtil.NearlyEqual(_targetRefreshTimer, 0f))
            {
                _targetRefreshTimer += Time.fixedDeltaTime;
            }

            var targetPosition = _currentTarget != null ? _currentTarget.Transform.position : (Vector3?) null;
            if (targetPosition.HasValue)
            {
                Aim(targetPosition.Value);
            }
        }

        private void Aim(Vector3 targetPosition)
        {
            var targetDir = Quaternion.LookRotation(targetPosition - turretRotatable.position);

            turretRotatable.rotation = Quaternion.RotateTowards(turretRotatable.rotation, targetDir, data.RotateSpeed * Time.fixedDeltaTime);
        }

        //Shoot as long as we have enemies - Kill On Sight Comrade
        public override bool ReceivedShootBulletInput()
        {
            return unitDetector.EnemiesInRange.Any();
        }

        public AiInterestCategory InterestCategory => AiInterestCategory.Turret;
        //it does not matter for a turret
        public Bounds Bounds => new Bounds(transform.position, Vector3.one);
    }
}