using UnityEngine;

namespace Movement.Handler
{
    //serve as proof of concept
    public class TransformBasedNoobMovementHandler : MovementHandler
    {
        private void FixedUpdate()
        {
            MoveVertical(inputSource.Vertical());
            MoveHorizontal(inputSource.Horizontal());
        }

        private void MoveVertical(float value)
        {
            transform.position += transform.forward * value;
        }

        private void MoveHorizontal(float value)
        {
            transform.Rotate(Vector3.up * value);
        }
    }
}