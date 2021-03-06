using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Bullet.InputSource;
using Common.Constant;
using Common.Enum;
using Common.Event;
using Elements.Turret.Upgrade;
using Elements.Units.UnitCommon;
using EventManagement;
using Rules;
using TrainingSpecific.Events;
using UnityEngine;
using UnityUtils;

namespace Elements.Turret
{
    [RequireComponent(typeof(UnitDetector))]
    public class Turret : Element, IHandle<ForceResetEvent>, IUpgradable
    {
        private const float UpdateTargetInterval = 0.5f;

        private Unit _currentTarget;
        private ITurretRepository _repository;
        private PooledMonoBehaviour _pooledBullet;
        private float _targetRefreshTimer;
        [SerializeField] private GenericShootService genericShootService;
        [SerializeField] private TurretProvider repositoryProvider;
        [SerializeField] private Transform turretRotatable;
        [SerializeField] private UnitDetector unitDetector;

        public override AiInterestCategory InterestCategory => AiInterestCategory.Turret;

        //it does not matter for a turret
        public override Bounds Bounds => new Bounds(transform.position, Vector3.one);

        public void Handle(ForceResetEvent @event)
        {
            if (!ReferenceEquals(@event.DynamicObjectOfInterest, this)) return;

            Destroy(gameObject);
        }

        public IEnumerable<Fact> Facts => _repository.Facts;

        protected override void OnEnable()
        {
            base.OnEnable();

            _repository = repositoryProvider.GetRepository();
            EventAggregator.Publish(new TurretSpawnedEvent(this));
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            EventAggregator.Publish(new TurretDestroyedEvent(this));
        }

        private void FixedUpdate()
        {
            if (_targetRefreshTimer > UpdateTargetInterval)
            {
                _currentTarget = _repository.TargetingStrategy.ChooseTarget(transform, unitDetector.EnemiesInRange);
                _targetRefreshTimer = 0f;
            }

            if (unitDetector.EnemiesInRange.Any() || FloatUtil.NearlyEqual(_targetRefreshTimer, 0f)) _targetRefreshTimer += Time.fixedDeltaTime;

            var targetPosition = _currentTarget != null ? _currentTarget.DynamicObjectTransform.position : (Vector3?) null;
            if (targetPosition.HasValue) Aim(targetPosition.Value);

            genericShootService.IsShooting = ShouldShoot();
        }

        private void Aim(Vector3 targetPosition)
        {
            var targetDir = Quaternion.LookRotation(targetPosition - turretRotatable.position);

            turretRotatable.rotation = Quaternion.RotateTowards(turretRotatable.rotation, targetDir, _repository.RotateSpeed * Time.fixedDeltaTime);
        }

        public void UpgradeFrom(GameObject newTurret)
        {
            UpgradeVisualEffect();

            var selfTransform = transform;
            
            newTurret.transform.position = selfTransform.position;
            newTurret.transform.rotation = selfTransform.rotation;
            
            Destroy(gameObject);
        }

        [Conditional(GameConfig.GameplayMode)]
        private void UpgradeVisualEffect()
        {
            //todo: implementation
        }

        //Shoot as long as we have enemies - Kill On Sight Comrade
        private bool ShouldShoot() => unitDetector.EnemiesInRange.Any();
    }
}