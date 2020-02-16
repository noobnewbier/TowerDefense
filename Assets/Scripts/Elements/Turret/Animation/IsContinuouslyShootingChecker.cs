using Bullet.InputSource;
using UnityEngine;
using UnityUtils.BooleanProviders;
using UnityUtils.Timers;

namespace Elements.Turret.Animation
{
    public class IsContinuouslyShootingChecker : StateRepresenter
    {
        [SerializeField] private ThresholdTimer durationToChangeStateThreshold;
        [SerializeField] private BulletsShooterInputSource shooterInputSource;

        private void FixedUpdate()
        {
            if (IsStateChanged && durationToChangeStateThreshold.TryResetIfPassedThreshold())
            {
                SetState(shooterInputSource.ReceivedShootBulletInput());
            }
        }

        private bool IsStateChanged => shooterInputSource.ReceivedShootBulletInput() != ProvideBoolean();
    }
}