using UnityEngine;

namespace Movement.Handler
{
    //serve as proof of concept
    public class TransformBasedMovementHandler : MovementHandler
    {
        protected override void MoveVertical(float value)
        {
            transform.Translate(transform.forward * value);
        }

        protected override void MoveHorizontal(float value)
        {
            transform.Rotate(Vector3.up * value);
        }
    }
}