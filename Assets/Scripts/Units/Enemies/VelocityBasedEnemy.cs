using Units.Enemies.Data;
using Units.UnitCommon;
using UnityEngine;

namespace Units.Enemies
{
    public class VelocityBasedEnemy : Unit, IHasRotation, IHasAcceleration
    {
        [SerializeField] private VelocityBasedUnitData data;

        protected override UnitData UnitData
        {
            get => data;
            set => data = (VelocityBasedUnitData) value;
        }

        protected override void Dies()
        {
            throw new System.NotImplementedException();
        }

        public float RotationSpeed => data.RotationSpeed;
        public float Acceleration => data.Acceleration;
    }
}