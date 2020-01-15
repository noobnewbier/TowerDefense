using System;
using Elements.Units.UnitCommon;
using UnityEngine;

namespace Movement.Handler
{
    public class VelocityBasedMovementHandler : MovementHandler
    {
        [SerializeField] private UnitProvider provider;
        [SerializeField] private Transform transformToMove;

        private IUnitDataRepository _repository;


        private void OnEnable()
        {
            _repository = provider.ProvideUnitDataRepository();
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

            MoveVertical(vertical);
            MoveHorizontal(horizontal);
        }

        private void MoveVertical(float inputValue)
        {
            var speed = inputValue < 0 ? _repository.MaxBackwardSpeed : _repository.MaxForwardSpeed;
            var velocity = inputValue * speed * transformToMove.forward;

            transformToMove.localPosition += velocity * Time.fixedDeltaTime;
        }

        private void MoveHorizontal(float inputValue)
        {
            transformToMove.Rotate(_repository.RotationSpeed * inputValue * Vector3.up);
        }
    }
}