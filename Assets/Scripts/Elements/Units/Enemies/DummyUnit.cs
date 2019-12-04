using Common.Enum;
using Common.Event;
using Elements.Units.UnitCommon;
using UnityEngine;

namespace Elements.Units.Enemies
{
    public class DummyUnit : Enemy
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
    }
}