using System;
using Elements.Units.Enemies.VelocityBased;
using UnityEngine;

namespace Movement.Handler
{
    public class VelocityBasedMovementHandler : MovementHandler
    {
        private float _acceleration;
        private float _deceleration;
        private float _rotationSpeed;
        private float _maxSpeed;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private VelocityBasedDataServiceAndRepositoryProvider provider;
        

        private void OnEnable()
        {
            var repository = provider.ProvideUnitDataRepository();
            
            _rotationSpeed = repository.RotationSpeed;
            _acceleration = repository.Acceleration;
            _deceleration = repository.Deceleration;
            _maxSpeed = repository.MaxSpeed;
        }

        private void FixedUpdate()
        {
            var horizontal = inputSource.Horizontal();
            var vertical = inputSource.Vertical();
#if DEBUG
            if (horizontal > 1f || horizontal < -1f)
            {
                throw new ArgumentOutOfRangeException($"Horizontal: {horizontal} cannot be greater than 1 or smaller than 0");
            }
            if (vertical > 1f || vertical < -1f)
            {
                throw new ArgumentOutOfRangeException($"Vertical: {vertical} cannot be greater than 1 or smaller than 0");
            }      
#endif
            
            MoveVertical(vertical, _acceleration);
            MoveHorizontal(horizontal, _rotationSpeed);
        }

        private void MoveVertical(float inputValue, float forwardSpeed)
        {
            var speed = inputValue < 0 ? _deceleration : _acceleration;
            var velocity = rb.velocity;
            velocity += inputValue * speed * transform.forward;
            
            rb.velocity = Vector3.ClampMagnitude(velocity, _maxSpeed);
        }

        private void MoveHorizontal(float inputValue, float rotationSpeed)
        {
            rb.transform.Rotate(rotationSpeed * inputValue * Vector3.up);
        }
    }
}