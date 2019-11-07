using Units.UnitCommon;
using UnityEngine;

namespace Units.Enemies.Data
{
    [CreateAssetMenu(menuName= "Data/VelocityBasedUnitData")]
    public class VelocityBasedUnitData : UnitData
    {
        [SerializeField] private float acceleration;

        public float Acceleration => acceleration;

        public float RotationSpeed => rotationSpeed;

        [SerializeField] private float rotationSpeed;
    }
}