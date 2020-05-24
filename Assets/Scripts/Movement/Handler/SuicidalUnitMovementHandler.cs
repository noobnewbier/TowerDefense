using System;
using Elements.Units.Enemies.Suicidal;
using Elements.Units.UnitCommon;
using UnityEngine;

namespace Movement.Handler
{
    public class SuicidalUnitMovementHandler : MovementHandler
    {
        private IUnitDataRepository _repository;
        private Transform _rigidBodyTransform;
        //we record our own forward vector as we are never actually rotating the transform
        private Vector3 _initialForward;
        private Quaternion _initialRotation;
        private SuicidalEnemy _suicidalEnemy;
        [SerializeField] private UnitProvider provider;
        [SerializeField] private Rigidbody rigidBodyToMove;


        private void OnEnable()
        {
            _repository = provider.ProvideUnitDataRepository();
            _rigidBodyTransform = rigidBodyToMove.transform;
            _initialForward = _rigidBodyTransform.forward;
            _initialRotation = _rigidBodyTransform.rotation;
            //todo: hack 1
            _suicidalEnemy = (SuicidalEnemy) provider.ProvideUnit();
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

            //todo: hack 2 -- no idea why the thing is rotating but this should fix it
            _rigidBodyTransform.rotation = _initialRotation;
        }

        private void MoveVertical(float inputValue)
        {
            var speed = inputValue < 0 ? _repository.MaxBackwardSpeed : _repository.MaxForwardSpeed;
            var velocity = inputValue * speed * (_suicidalEnemy.RealRotation * _initialForward);
            var newPosition = _rigidBodyTransform.position + velocity * Time.fixedDeltaTime;
            rigidBodyToMove.MovePosition(newPosition);
        }

        private void MoveHorizontal(float inputValue)
        {
            _suicidalEnemy.RealRotation = Quaternion.AngleAxis(
                _suicidalEnemy.RealRotation.eulerAngles.y + _repository.RotationSpeed * inputValue * Time.fixedDeltaTime,
                Vector3.up
            );
        }   
    }
}