using Effects;
using Elements.Units.UnitCommon;
using UnityEngine;

namespace Elements.Units.Enemies.VelocityBased
{
    [CreateAssetMenu(menuName = "Data/VelocityBasedUnitData")]
    public class VelocityBasedUnitData : UnitData
    {
        [SerializeField] private float acceleration;
        [SerializeField] private Effect damageEffect;
        [SerializeField] private Effect selfEffect;
        [SerializeField] private float deceleration;
        [SerializeField] private float rotationSpeed;

        public Effect SelfEffect => selfEffect;

        public Effect DamageEffect => damageEffect;

        public float Acceleration => acceleration;

        public float Deceleration => deceleration;

        public float RotationSpeed => rotationSpeed;
    }
}