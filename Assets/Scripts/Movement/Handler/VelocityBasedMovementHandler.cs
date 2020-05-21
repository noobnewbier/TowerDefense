using System;
using Elements.Units.UnitCommon;
using UnityEngine;

namespace Movement.Handler
{
    public class VelocityBasedMovementHandler : MovementHandler
    {
        private IUnitDataRepository _repository;
        private Transform _rigidBodyTransform;
        [SerializeField] private UnitProvider provider;
        [SerializeField] private Rigidbody rigidBodyToMove;


        private void OnEnable()
        {
            _repository = provider.ProvideUnitDataRepository();
            _rigidBodyTransform = rigidBodyToMove.transform;
        }

        private void FixedUpdate()
        {
            var horizontal = inputSource.Horizontal();
            var vertical = inputSource.Vertical();
            
#if DEBUG
            if (horizontal > 1f || horizontal < -1f)
                throw new ArgumentOutOfRangeException(
                    $"Horizontal: {horizontal} cannot be greater than 1 or smaller than -1"
                );

            if (vertical > 1f || vertical < -1f)
                throw new ArgumentOutOfRangeException(
                    $"Vertical: {vertical} cannot be greater than 1 or smaller than -1"
                );
#endif

            MoveVertical(vertical);
            MoveHorizontal(horizontal);
        }

        private void MoveVertical(float inputValue)
        {
            var speed = inputValue < 0 ? _repository.MaxBackwardSpeed : _repository.MaxForwardSpeed;
            var velocity = inputValue * speed * _rigidBodyTransform.forward;
            var newPosition = _rigidBodyTransform.position + velocity * Time.fixedDeltaTime;
            rigidBodyToMove.MovePosition(newPosition);
        }

        private void MoveHorizontal(float inputValue)
        {
            var newRotation = Quaternion.AngleAxis(
                rigidBodyToMove.rotation.eulerAngles.y + _repository.RotationSpeed * inputValue * Time.fixedDeltaTime,
                Vector3.up
            );
            rigidBodyToMove.MoveRotation(newRotation);
        }
    }
}