using UnityEngine;

namespace Turrets.Placement.InputSource
{
    public abstract class TurretPlacementInputSource : MonoBehaviour
    {
        public abstract bool ReceivedPlaceTurretInput();
    }
}