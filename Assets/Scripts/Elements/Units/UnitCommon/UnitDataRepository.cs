using System.Collections.Generic;
using Rules;

namespace Elements.Units.UnitCommon
{
    public interface IUnitDataRepository
    {
        float Health { get; }
        float MaxForwardSpeed { get; }
        IEnumerable<Fact> Facts { get; }
        float MaxBackwardSpeed { get; }
        float RotationSpeed { get; }
        float MaxHealth { get; }
    }

    public class UnitDataRepository : IUnitDataRepository
    {
        private readonly UnitData _unitData;

        public UnitDataRepository(UnitData unitData)
        {
            _unitData = unitData;
        }

        public float Health => _unitData.Health;
        public float MaxForwardSpeed => _unitData.MaxForwardSpeed;
        public float MaxBackwardSpeed => _unitData.MaxBackwardSpeed;
        public float RotationSpeed => _unitData.RotationSpeed;
        public float MaxHealth => _unitData.MaxHealth;
        public IEnumerable<Fact> Facts => _unitData.Facts;
    }
}