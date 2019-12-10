using Common.Enum;
using Common.Event;
using Elements.Units.Enemies.Data;
using Elements.Units.UnitCommon;
using UnityEngine;

namespace Elements.Units.Enemies
{
    //this is a dummy that can potentially fit into anything, if configured correctly
    public class DummyUnit : Enemy, IHasRotation, IMoveByVelocity
    {
        [SerializeField] private UnitData unitData;

        protected override UnitData UnitData
        {
            get => unitData;
            set => unitData = value;
        }

        public override AiInterestCategory InterestCategory => AiInterestCategory.Enemy;

        protected override void DeathVisualEffect()
        {
            // do nothing
        }

        public float RotationSpeed => ((VelocityBasedUnitData) unitData).RotationSpeed;
        public float Acceleration => ((VelocityBasedUnitData) unitData).Acceleration;
        public float MaxSpeed => ((VelocityBasedUnitData) unitData).MaxSpeed;
        public Rigidbody Rigidbody => GetComponent<Rigidbody>();
    }
}