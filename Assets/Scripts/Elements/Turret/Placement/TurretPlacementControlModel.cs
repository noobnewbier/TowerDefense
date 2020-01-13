using UnityEngine;

namespace Elements.Turret.Placement
{
    [CreateAssetMenu(menuName = "Data/TurretPlacementControlModel")]
    public class TurretPlacementControlModel : ScriptableObject
    {
        [SerializeField] private TurretProvider turretProvider;
        public int TurretPrice => turretProvider.GetRepository().Cost;
        public GameObject CopyOfTurret => turretProvider.GetTurretPrefab();
    }
}