using Common.Enum;
using Elements.Units.UnitCommon;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(menuName = "TemporaryEffect/SpeedEffect")]
    public class SpeedEffect : Effect
    {
        [SerializeField] private int duration;
        public override int Duration => duration;

        public override void FirstEffectApply(IUnitDataService service, IUnitDataRepository dataRepository, EffectSource effectSource)
        {
            base.FirstEffectApply(service, dataRepository, effectSource);

            service.ModifySpeed(modifier.ModifyValue(dataRepository.MaxSpeed));
        }

        public override void EndEffect(IUnitDataService service, IUnitDataRepository dataRepository, EffectSource effectSource)
        {
            base.EndEffect(service, dataRepository, effectSource);

            service.ModifySpeed(modifier.RevertValue(dataRepository.MaxSpeed));
        }
    }
}