using UnityEngine;

namespace Elements.Turret.Placement.InputSource
{
    public interface ITurretPlacementInputSource
    {
        bool ReceivedPlaceTurretInput();
        bool ReceivedPendingTurretPlacementInput();
    }

    public abstract class TurretPlacementInputSource : MonoBehaviour, ITurretPlacementInputSource
    {
        public abstract bool ReceivedPlaceTurretInput();
        public abstract bool ReceivedPendingTurretPlacementInput();
    }
}