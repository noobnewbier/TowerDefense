using UnityEngine;

namespace Bullet.InputSource
{
    public class PlayerBulletShooterInputSource : BulletsShooterInputSource
    {
        public override bool ReceivedShootBulletInput()
        {
            return Input.GetMouseButton(1);
        }
    }
}