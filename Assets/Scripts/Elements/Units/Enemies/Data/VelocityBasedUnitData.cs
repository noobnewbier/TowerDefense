using Elements.Units.UnitCommon;
using UnityEngine;

namespace Elements.Units.Enemies.Data
{
    [CreateAssetMenu(menuName = "Data/VelocityBasedUnitData")]
    public class VelocityBasedUnitData : UnitData
    {
        [SerializeField] private float acceleration;
        [SerializeField] private int damage;
        [SerializeField] private float rotationSpeed;

        public int Damage => damage;

        public float Acceleration => acceleration;

        public float RotationSpeed => rotationSpeed;
    }
}