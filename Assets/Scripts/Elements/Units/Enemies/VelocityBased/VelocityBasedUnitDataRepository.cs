using Effects;
using Elements.Units.UnitCommon;

namespace Elements.Units.Enemies.VelocityBased
{
    public interface IVelocityBasedUnitDataRepository : IUnitDataRepository
    {
        float Acceleration { get; }
        float Deceleration { get; }
        float RotationSpeed { get; }
        Effect DamageEffect { get; }
    }

    public class VelocityBasedUnitDataRepository : UnitDataRepository, IVelocityBasedUnitDataRepository
    {
        private readonly VelocityBasedUnitData _data;

        public VelocityBasedUnitDataRepository(VelocityBasedUnitData unitData) : base(unitData)
        {
            _data = unitData;
        }

        public float Acceleration => _data.Acceleration;
        public float Deceleration => _data.Deceleration;
        public float RotationSpeed => _data.RotationSpeed;
        public Effect DamageEffect => _data.DamageEffect;
    }
}