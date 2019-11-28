using Elements.Units;
using UnityEngine;

namespace Movement.Handler
{
    public class VelocityBasedMovementHandler : MovementHandler
    {
        private float _acceleration;
        private float _rotationSpeed;
        private float _maxSpeed;
        [SerializeField] private Rigidbody rb;

        private void OnEnable()
        {
            _rotationSpeed = ((IHasRotation) unit).RotationSpeed;
            _acceleration = ((IMoveByVelocity) unit).Acceleration;
            _maxSpeed = ((IMoveByVelocity) unit).MaxSpeed;
        }

        private void FixedUpdate()
        {
            MoveVertical(inputSource.Vertical(), _acceleration);
            MoveHorizontal(inputSource.Horizontal(), _rotationSpeed);
        }

        private void MoveVertical(float inputValue, float forwardSpeed)
        {
            var velocity = rb.velocity;
            velocity += inputValue * forwardSpeed * transform.forward;
            
            rb.velocity = Vector3.ClampMagnitude(velocity, _maxSpeed);
        }

        private void MoveHorizontal(float inputValue, float rotationSpeed)
        {
            rb.transform.Rotate(rotationSpeed * inputValue * Vector3.up);
        }
    }
}