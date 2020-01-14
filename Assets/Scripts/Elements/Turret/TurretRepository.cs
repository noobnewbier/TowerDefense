using System.Collections.Generic;
using Elements.Turret.TargetingPicking;
using Rules;
using UnityEngine;

namespace Elements.Turret
{
    public interface ITurretRepository
    {
        float DamageForUiDisplay { get; }
        float RotateSpeed { get; }
        IEnumerable<Fact> Facts { get; }
        float DetectionRange { get; }
        TargetingStrategy TargetingStrategy { get; }
        IBulletShooterRepository BulletShooterRepository { get; }
        int Cost { get; }
        string Description { get; }
        string Name { get; }
    }

    public class TurretRepository : ITurretRepository
    { 
        private readonly TurretData _turretData;
        private readonly IBulletShooterRepository _bulletShooterRepository;

        public TurretRepository(IBulletShooterRepository bulletShooterRepository, TurretData turretData)
        {
            _bulletShooterRepository = bulletShooterRepository;
            _turretData = turretData;
        }

        public IBulletShooterRepository BulletShooterRepository => _bulletShooterRepository;
        public int Cost => _turretData.Cost;
        public string Description => _turretData.Description;
        public string Name => _turretData.TurretName;
        public float DamageForUiDisplay => _turretData.DamageForUiDisplay;
        public float RotateSpeed => _turretData.RotateSpeed;
        public IEnumerable<Fact> Facts => _turretData.Facts;
        public float DetectionRange => _turretData.DetectionRange;
        public TargetingStrategy TargetingStrategy => _turretData.TargetingStrategy;
    }
}