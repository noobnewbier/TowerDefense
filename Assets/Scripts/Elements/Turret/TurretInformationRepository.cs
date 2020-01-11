using System.Collections.Generic;
using Bullet;
using Elements.Turret.TargetingPicking;
using Rules;
using UnityEngine;
using UnityUtils;

namespace Elements.Turret
{
    [CreateAssetMenu(menuName = "ScriptableService/TurretInformationRepository")]
    public class TurretInformationRepository : ScriptableObject
    {
        [SerializeField] private BulletShooterData bulletShooterData;
        [SerializeField] private TurretData turretData;

        public float ShootFrequency => bulletShooterData.ShootFrequency;

        //we don't actually have access to how much damage a turret does, it wouldn't be possible to calculate either as they can be poisoning enemies etc. Instead we "fake" a damage value so we can display it in the UI
        public float DamageValueInUI => 0f;
        public float RotateSpeed => turretData.RotateSpeed;
        public IEnumerable<Fact> Facts => turretData.Facts;
        public float DetectionRange => turretData.DetectionRange;
        public TargetingStrategy TargetingStrategy => turretData.TargetingStrategy;
        public PooledMonoBehaviour PooledBullet => bulletShooterData.Bullet.GetComponent<PooledMonoBehaviour>();
    }
}