using Units.UnitCommon;
using UnityEngine;

namespace Units.Enemies.Data
{
    public class VelocityBasedUnitData : UnitData
    {
        [SerializeField] private float acceleration;

        public float Acceleration => acceleration;

        public float RotationSpeed => rotationSpeed;

        [SerializeField] private float rotationSpeed;
    }
}