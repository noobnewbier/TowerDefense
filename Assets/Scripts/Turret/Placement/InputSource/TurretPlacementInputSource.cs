using UnityEngine;

namespace Turret.Placement.InputSource
{
    public abstract class TurretPlacementInputSource : MonoBehaviour
    {
        public abstract bool ReceivedPlaceTurretInput();
    }
}