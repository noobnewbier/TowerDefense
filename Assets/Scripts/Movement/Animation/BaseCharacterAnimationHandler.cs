using Bullet.InputSource;
using Elements.Turret;
using Elements.Turret.Placement.InputSource;
using Movement.InputSource;
using UnityEngine;
using UnityUtils;

namespace Movement.Animation
{
    public class BaseCharacterAnimationHandler : MonoBehaviour
    {
        private static readonly int IsShooting = Animator.StringToHash("IsShooting");
        private static readonly int PlaceTurret = Animator.StringToHash("PlaceTurret");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int ShootFrequency = Animator.StringToHash("ShootFrequency");


        private IBulletShooterRepository _bulletShooterRepository;
        [SerializeField] private Animator animator;
        [Range(0f, 5f)] [SerializeField] private float easingParameter = 2;
        [SerializeField] private float animSpeed = 1.0f;
        [SerializeField] private BulletShooterRepositoryProvider bulletShooterRepositoryProvider;
        [SerializeField] private MovementInputSource movementInputSource;
        [SerializeField] private BulletsShooterInputSource shooterInputSource;
        [SerializeField] private Timer shootTimer;
        [SerializeField] private TurretPlacementInputSource turretPlacementInputSource;

        private void OnEnable()
        {
            _bulletShooterRepository = bulletShooterRepositoryProvider.ProvideRepository();
        }

        private void FixedUpdate()
        {
            if (turretPlacementInputSource.ReceivedPlaceTurretInput()) animator.SetTrigger(PlaceTurret);

            animator.SetBool(IsShooting, shooterInputSource.ReceivedShootBulletInput());

            animator.SetFloat(Speed, movementInputSource.Vertical());
            animator.SetFloat(ShootFrequency, ExponentialEaseOut(shootTimer.NormalizedTime));

            animator.speed = animSpeed;
        }

        private float ExponentialEaseOut(float x)
        {
            var y = 1 - Mathf.Pow(1 - x, easingParameter);
            return y;
        }
    }
}