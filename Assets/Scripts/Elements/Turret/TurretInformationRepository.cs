using Bullet;

namespace Elements.Turret
{
    public class TurretInformationRepository
    {
        private readonly BulletData _bulletData;

        private readonly BulletShooterData _bulletShooterData;

        //will refactor into one
        private readonly TurretData _turretData;

        public TurretInformationRepository(BulletData bulletData, BulletShooterData bulletShooterData, TurretData turretData)
        {
            _bulletData = bulletData;
            _bulletShooterData = bulletShooterData;
            _turretData = turretData;
        }

        public float ShootFrequency => _bulletShooterData.ShootFrequency;

        //we don't actually have access to how much damage a turret does, it wouldn't be possible to calculate either as they can be poisoning enemies etc. Instead we "fake" a damage value so we can display it in the UI
        public float DamageValueInUI => 0f;
        public float Range => _bulletData.Range;
        public float RotateSpeed => _turretData.RotateSpeed;
    }
}