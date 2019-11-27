using UnityEngine;

namespace Elements.Turret.Placement.InputSource
{
    public class PlayerTurretPlacementInputSource : TurretPlacementInputSource
    {
        public override bool ReceivedPlaceTurretInput()
        {
            return Input.GetMouseButtonUp(0);
        }
    }
}