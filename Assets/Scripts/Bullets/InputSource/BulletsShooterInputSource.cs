using UnityEngine;

namespace Bullets.InputSource
{
    public abstract class BulletsShooterInputSource : MonoBehaviour
    {
        public abstract bool ReceivedShootBulletInput();
    }
}