using Common.Enum;
using Elements.Units.UnitCommon;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(menuName = "InstantEffect/HealthEffect")]
    public class HealthInstantEffect : Effect
    {
        public override bool CanStack => true;
        public override int Duration => 0;

        public override void FirstEffectApply(IUnitDataService service, IUnitDataRepository dataRepository, EffectSource effectSource)
        {
            base.FirstEffectApply(service, dataRepository, effectSource);

            service.ModifyHealth((int) modifier.ModifyValue(dataRepository.Health), effectSource);
        }
    }
}