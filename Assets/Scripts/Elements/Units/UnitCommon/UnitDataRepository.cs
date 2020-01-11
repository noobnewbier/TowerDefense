using System.Collections.Generic;
using Rules;

namespace Elements.Units.UnitCommon
{
    public interface IUnitDataRepository
    {
        int Health { get; }
        float MaxForwardSpeed { get; }
        IEnumerable<Fact> Facts { get; }
        float MaxBackwardSpeed { get; }
        float RotationSpeed { get; }
    }

    public class UnitDataRepository : IUnitDataRepository
    {
        private readonly UnitData _unitData;

        public UnitDataRepository(UnitData unitData)
        {
            _unitData = unitData;
        }

        public int Health => _unitData.Health;
        public float MaxForwardSpeed => _unitData.MaxForwardSpeed;
        public float MaxBackwardSpeed => _unitData.MaxBackwardSpeed;
        public float RotationSpeed => _unitData.RotationSpeed;
        public IEnumerable<Fact> Facts => _unitData.Facts;
    }
}