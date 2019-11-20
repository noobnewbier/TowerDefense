using UnityEngine;

namespace Turret.Placement.InputSource
{
    public class PlayerTurretPlacementInputSource : TurretPlacementInputSource
    {
        public override bool ReceivedPlaceTurretInput() => Input.GetMouseButtonUp(0);
    }
}