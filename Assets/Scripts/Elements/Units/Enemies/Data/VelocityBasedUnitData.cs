using Elements.Units.UnitCommon;
using UnityEngine;

namespace Elements.Units.Enemies.Data
{
    [CreateAssetMenu(menuName = "Data/VelocityBasedUnitData")]
    public class VelocityBasedUnitData : UnitData
    {
        [field: SerializeField] public int Damage { get; }

        [field: SerializeField] public float Acceleration { get; }

        [field: SerializeField] public float RotationSpeed { get; }
    }
}