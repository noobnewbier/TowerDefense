using Common.Enum;
using Elements.Units.UnitCommon;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(menuName = "TemporaryEffect/SpeedEffect")]
    public class SpeedEffect : Effect
    {
        [SerializeField] private int duration;
        public override bool CanStack => false;
        public override int Duration => duration;

        public override void FirstEffectApply(IUnitDataModificationService modificationService, IUnitDataRepository dataRepository, EffectSource effectSource)
        {
            base.FirstEffectApply(modificationService, dataRepository, effectSource);

            modificationService.ModifyForwardSpeed(modifier.ModifyValue(dataRepository.MaxForwardSpeed));
            modificationService.ModifyBackwardSpeed(modifier.ModifyValue(dataRepository.MaxBackwardSpeed));
        }

        public override void EndEffect(IUnitDataModificationService modificationService, IUnitDataRepository dataRepository, EffectSource effectSource)
        {
            base.EndEffect(modificationService, dataRepository, effectSource);

            modificationService.ModifyForwardSpeed(modifier.RevertValue(dataRepository.MaxForwardSpeed));
            modificationService.ModifyBackwardSpeed(modifier.RevertValue(dataRepository.MaxBackwardSpeed));
        }
    }
}