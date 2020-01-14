using Experimental;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Elements.Turret.Upgrade.UI.Option.Entry
{
    public interface IUpgradeOptionModel
    {
        int Cost { get; }
        string Name { get; }
        string Description { get; }
        float DamageForUiDisplay { get; }
        float DetectionRange { get; }
        float ShootFrequency { get; }
        TurretUpgradeEntry TurretUpgradeEntry { get; set; }
        bool HasOption { get; }
    }

    [CreateAssetMenu(menuName = "ScriptableModel/UpgradeOption")]
    public class UpgradeOptionModel : ScriptableObject, IUpgradeOptionModel
    {
        private TurretUpgradeEntry _turretUpgradeEntry;
        [SerializeField] private EventAggregatorProvider eventAggregatorProvider;

        public float DetectionRange => _turretUpgradeEntry.TurretRepository.DetectionRange;
        public float ShootFrequency => _turretUpgradeEntry.TurretRepository.BulletShooterRepository.ShootFrequency;

        public TurretUpgradeEntry TurretUpgradeEntry
        {
            get => _turretUpgradeEntry;
            set
            {
                _turretUpgradeEntry = value;
                eventAggregatorProvider.ProvideEventAggregator().Publish(new UpgradeOptionModelUpdateEvent(this));
            }
        }

        public bool HasOption => _turretUpgradeEntry != null;
        public string Description => _turretUpgradeEntry.TurretRepository.Description;
        public float DamageForUiDisplay => _turretUpgradeEntry.TurretRepository.DamageForUiDisplay;
        public int Cost => _turretUpgradeEntry.TurretRepository.Cost;
        public string Name => _turretUpgradeEntry.TurretRepository.Name;
    }
}