using UnityEngine;

namespace Movement.Handler
{
    //serve as proof of concept
    public class TransformBasedMovementHandler : MovementHandler
    {
        private void FixedUpdate()
        {
            MoveVertical(inputSource.Vertical());
            MoveHorizontal(inputSource.Horizontal());
        }

        private void MoveVertical(float value)
        {
            transform.Translate(transform.forward * value);
        }

        private void MoveHorizontal(float value)
        {
            transform.Rotate(Vector3.up * value);
        }
    }
}