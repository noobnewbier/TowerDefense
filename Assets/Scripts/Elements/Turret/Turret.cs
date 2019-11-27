using System.Linq;
using Bullet.InputSource;
using Common.Enum;
using Elements.Units.UnitCommon;
using UnityEngine;
using UnityUtils;

namespace Elements.Turret
{
    [RequireComponent(typeof(UnitDetector))]
    public class Turret : Element
    {
        private const float UpdateTargetInterval = 0.5f;

        private Unit _currentTarget;

        private PooledMonoBehaviour _pooledBullet;
        private float _targetRefreshTimer;

        [SerializeField] private TurretData data;
        [SerializeField] private GenericShootService genericShootService;
        [SerializeField] private Transform turretRotatable;
        [SerializeField] private UnitDetector unitDetector;

        public override AiInterestCategory InterestCategory => AiInterestCategory.Turret;

        //it does not matter for a turret
        public override Bounds Bounds => new Bounds(transform.position, Vector3.one);

        private void Awake()
        {
            unitDetector.Initialize(data);
        }

        private void FixedUpdate()
        {
            if (_targetRefreshTimer > UpdateTargetInterval)
            {
                _currentTarget = data.TargetingStrategy.ChooseTarget(transform, unitDetector.EnemiesInRange);
                _targetRefreshTimer = 0f;
            }

            if (unitDetector.EnemiesInRange.Any() || FloatUtil.NearlyEqual(_targetRefreshTimer, 0f)) _targetRefreshTimer += Time.fixedDeltaTime;

            var targetPosition = _currentTarget != null ? _currentTarget.Transform.position : (Vector3?) null;
            if (targetPosition.HasValue) Aim(targetPosition.Value);

            genericShootService.IsShooting = ShouldShoot();
        }

        private void Aim(Vector3 targetPosition)
        {
            var targetDir = Quaternion.LookRotation(targetPosition - turretRotatable.position);

            turretRotatable.rotation = Quaternion.RotateTowards(turretRotatable.rotation, targetDir, data.RotateSpeed * Time.fixedDeltaTime);
        }

        //Shoot as long as we have enemies - Kill On Sight Comrade
        private bool ShouldShoot()
        {
            return unitDetector.EnemiesInRange.Any();
        }
    }
}