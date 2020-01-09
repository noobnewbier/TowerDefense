using Rules;

namespace Elements.Units.UnitCommon
{
    public interface IUnitDataRepository
    {
        int Health { get; }
        float MaxSpeed { get; }
        Fact[] Facts { get; }
    }

    public class UnitDataRepository : IUnitDataRepository
    {
        private readonly UnitData _unitData;

        public UnitDataRepository(UnitData unitData)
        {
            _unitData = unitData;
        }

        public int Health => _unitData.Health;
        public float MaxSpeed => _unitData.MaxSpeed;
        public Fact[] Facts => _unitData.Facts;
    }
}