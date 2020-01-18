using UnityEngine;

namespace Elements.Turret.Placement.InputSource
{
    public class PlayerTurretPlacementInputSource : TurretPlacementInputSource
    {
        public override bool ReceivedPlaceTurretInput() => Input.GetKeyUp("q");
        public override bool ReceivedPendingTurretPlacementInput() => Input.GetAxis("PlaceTurret") > 0;
    }
}