using UnityEngine;

namespace Bullet.InputSource
{
    public abstract class BulletsShooterInputSource : MonoBehaviour
    {
        public abstract bool ReceivedShootBulletInput();
    }
}