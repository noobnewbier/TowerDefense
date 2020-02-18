using UnityEngine;

namespace Elements.Turret.Placement
{
    [CreateAssetMenu(menuName = "Data/TurretPlacementControlModel")]
    public class TurretPlacementControlModel : ScriptableObject
    {
        [SerializeField] private TurretProvider turretProvider;

        public TurretProvider TurretProvider => turretProvider;
    }
}