using UnityEngine;

namespace Movement.InputSource
{
    public class PlayerMovementInputSource : MovementInputSource
    {
        public override float Vertical()
        {
            return Input.GetAxis("Vertical");
        }

        public override float Horizontal()
        {
            return Input.GetAxis("Horizontal");
        }
    }
}