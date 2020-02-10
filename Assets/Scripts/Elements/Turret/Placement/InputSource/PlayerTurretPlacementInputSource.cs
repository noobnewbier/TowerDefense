using UnityEngine;

namespace Elements.Turret.Placement.InputSource
{
    public class PlayerTurretPlacementInputSource : TurretPlacementInputSource
    {
        public override bool ReceivedPlaceTurretInput() => Input.GetButtonUp("PlaceTurret");
        public override bool ReceivedPendingTurretPlacementInput() => Input.GetAxis("PlaceTurret") > 0;
    }
}