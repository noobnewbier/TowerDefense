using System;
using Units;
using UnityEngine;

namespace Movement.Handler
{
    public class VelocityBasedMovementHandler : MovementHandler
    {
        private float _rotationSpeed;
        private float _acceleration;
        
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
            transform.Translate(transform.forward * inputValue);
        }

        private void MoveHorizontal(float inputValue, float rotationSpeed)
        {
            transform.Rotate(rotationSpeed * inputValue * Vector3.up);
        }
    }
}