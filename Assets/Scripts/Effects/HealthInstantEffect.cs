using Common.Enum;
using Effects.Modifiers;
using Elements.Units.UnitCommon;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(menuName = "InstantEffect/HealthEffect")]
    public class HealthInstantEffect : Effect
    {
        public override bool CanStack => true;
        public override int Duration => 0;

        public override void FirstEffectApply(IUnitDataModificationService modificationService,
                                              IUnitDataRepository dataRepository,
                                              EffectSource effectSource)
        {
            base.FirstEffectApply(modificationService, dataRepository, effectSource);

            modificationService.ModifyHealth(modifier.ModifyValue(dataRepository.Health), effectSource);
        }

        /// <summary>
        /// dirty, as you don't want to rewrite the hierarchy(well it doesn't take that long, but still)
        /// shall the need arise re-write the hierarchy so Effect no longer inherit from SO,
        /// instead having a EffectProvider that is a SO but provides a POCO of Effect
        /// </summary>
        public static HealthInstantEffect CreateInstantHealthEffect(Modifier modifier)
        {
            var toReturn = CreateInstance<HealthInstantEffect>();
            toReturn.modifier = modifier;

            return toReturn;
        }
    }
}