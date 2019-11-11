using UnityEngine;

namespace Bullets.InputSource
{
    public class PlayerBulletShooterInputSource : BulletsShooterInputSource
    {
        public override bool ReceivedShootBulletInput()
        {
            return Input.GetMouseButtonDown(1);
        }
    }
}