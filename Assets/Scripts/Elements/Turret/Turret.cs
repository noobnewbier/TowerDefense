using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Bullet.InputSource;
using Common.Constant;
using Common.Enum;
using Common.Event;
using Elements.Turret.Rotation;
using Elements.Turret.Upgrade;
using Elements.Units.UnitCommon;
using EventManagement;
using Rules;
using TrainingSpecific.Events;
using UnityEngine;
using UnityEngine.Serialization;
using UnityUtils;

namespace Elements.Turret
{
    public class Turret : Element, IHandle<ForceResetEvent>, IUpgradable
    {
        private const float UpdateTargetInterval = 0.5f;

        private Unit _currentTarget;
        private PooledMonoBehaviour _pooledBullet;
        private ITurretRepository _repository;
        private float _targetRefreshTimer;
        [SerializeField] private GenericShootService genericShootService;
        [SerializeField] private Rotator rotator;

        [FormerlySerializedAs("repositoryProvider")] [SerializeField]
        private TurretProvider turretProvider;

        [SerializeField] private UnitDetector unitDetector;
        protected override InterestCategory Category => InterestCategory.Turret;
        public override Bounds Bounds => new Bounds(transform.position, Vector3.one);

        public void Handle(ForceResetEvent @event)
        {
            if (!ReferenceEquals(@event.DynamicObjectOfInterest, this)) return;

            Destroy(gameObject);
        }

        public IEnumerable<Fact> Facts => _repository.Facts;

        //don't really know how to best handle upgrades visual effect tbh
        public void Destruct()
        {
            DestructVisualEffect();
        }

        public void Construct()
        {
            ConstructVisualEffect();
        }

        public Transform CurrentTransform => transform;

        protected override void OnEnable()
        {
            base.OnEnable();

            _repository = turretProvider.GetRepository();
            EventAggregator.Publish(new TurretSpawnedEvent(this));
        }

        private void Start()
        {
            Construct();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            EventAggregator.Publish(new TurretDestroyedEvent(this));
        }

        private void FixedUpdate()
        {
            if (!CanShoot()) return;

            if (_targetRefreshTimer > UpdateTargetInterval)
            {
                _currentTarget = _repository.TargetingStrategy.ChooseTarget(transform, unitDetector.VisibleEnemies);
                _targetRefreshTimer = 0f;
            }

            _targetRefreshTimer += Time.fixedDeltaTime;

            var targetPosition =
                _currentTarget != null ? _currentTarget.ObjectTransform.position : (Vector3?) null;
            if (targetPosition.HasValue) Aim(targetPosition.Value);

            genericShootService.IsShooting = ShouldShoot();
        }

        private void Aim(Vector3 targetPosition)
        {
            rotator.LookAt(targetPosition, _repository.RotateSpeed);
        }

        [Conditional(GameConfig.GameplayMode)]
        private void DestructVisualEffect()
        {
            StartCoroutine(DestructVisualEffectCoroutine());
        }

        [Conditional(GameConfig.GameplayMode)]
        private void ConstructVisualEffect()
        {
            turretProvider.ConstructAnimationController.ConstructTurretAnimation();
        }

        private IEnumerator DestructVisualEffectCoroutine()
        {
            turretProvider.DestructAnimationController.DestructTurretAnimation();

            yield return new WaitUntil(() => !turretProvider.DestructAnimationController.IsPlayingAnimation);

            Destroy(gameObject);
        }

        //Shoot as long as we have enemies - Kill On Sight Comrade
        private bool ShouldShoot()
        {
            return unitDetector.VisibleEnemies.Any();
        }

        //cannot shoot during destruct or construct animation
        private bool CanShoot()
        {
            return !turretProvider.DestructAnimationController.IsPlayingAnimation &&
                   !turretProvider.ConstructAnimationController.IsPlayingAnimation;
        }
    }
}