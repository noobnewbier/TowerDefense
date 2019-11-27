using Elements.Units;
using UnityEngine;

namespace Movement.Handler
{
    public class VelocityBasedMovementHandler : MovementHandler
    {
        private float _acceleration;
        private float _rotationSpeed;
        [SerializeField] private Rigidbody rb;

        private void OnEnable()
        {
            _rotationSpeed = ((IHasRotation) unit).RotationSpeed;
            _acceleration = ((IHasAcceleration) unit).Acceleration;
        }

        private void FixedUpdate()
        {
            MoveVertical(inputSource.Vertical(), _acceleration);
            MoveHorizontal(inputSource.Horizontal(), _rotationSpeed);
        }

        private void MoveVertical(float inputValue, float forwardSpeed)
        {
            rb.velocity += inputValue * forwardSpeed * transform.forward;
        }

        private void MoveHorizontal(float inputValue, float rotationSpeed)
        {
            rb.transform.Rotate(rotationSpeed * inputValue * Vector3.up);
        }
    }
}