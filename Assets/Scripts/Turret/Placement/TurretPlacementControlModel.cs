using Shop;
using UnityEngine;

namespace Turret.Placement
{
    public interface ITurretPlacementControlModel //we will figure this out a bit later
    {
        GameObject CopyOfTurret { get; }
    }

    [CreateAssetMenu(menuName = "Data/TurretPlacementControlModel")]
    public class TurretPlacementControlModel : ScriptableObject, ITurretPlacementControlModel
    {
        [SerializeField] private TurretShopEntry turretShopEntry;
        public int TurretPrice => turretShopEntry.Price;

        public GameObject CopyOfTurret => Instantiate(turretShopEntry.TurretPrefab);
    }
}