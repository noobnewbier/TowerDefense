using Elements;

namespace Bullet.InputSource
{
    /// not very elegant, but essentially turret cannot inherit from the
    /// <see cref="BulletsShooterInputSource" />
    /// as it needs to inherit from
    /// <see cref="Element" />
    /// instead.
    public class GenericShootService : BulletsShooterInputSource
    {
        public bool IsShooting { get; set; }

        public override bool ReceivedShootBulletInput()
        {
            return IsShooting;
        }
    }
}