using UnityEngine;

namespace Elements.Turret.Placement.InputSource
{
    public abstract class TurretPlacementInputSource : MonoBehaviour
    {
        public abstract bool ReceivedPlaceTurretInput();
    }
}