using Shop;
using UnityEngine;

namespace Elements.Turret.Placement
{
    [CreateAssetMenu(menuName = "Data/TurretPlacementControlModel")]
    public class TurretPlacementControlModel : ScriptableObject
    {
        [SerializeField] private TurretShopEntry turretShopEntry;
        public int TurretPrice => turretShopEntry.Price;
        public GameObject CopyOfTurret => Instantiate(turretShopEntry.TurretPrefab);
    }
}