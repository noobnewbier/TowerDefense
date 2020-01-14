using Bullet;
using UnityEngine;

namespace Elements.Turret
{
    public class BulletShooterRepositoryProvider : MonoBehaviour
    {
        [SerializeField] private BulletShooterData shooterData;
        private IBulletShooterRepository _bulletShooterRepository;
        private IBulletShooterRepository BulletShooterRepository =>
            _bulletShooterRepository ?? (_bulletShooterRepository = new BulletShooterRepository(shooterData));

        public IBulletShooterRepository ProvideRepository() => BulletShooterRepository;
    }
}