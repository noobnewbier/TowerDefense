using System;
using Common.Enum;
using Elements.Units.UnitCommon;

namespace Effects
{
    public class ForceResetEffect : Effect
    {
        public static ForceResetEffect Instance => LazyInstance.Value;
        private static readonly Lazy<ForceResetEffect> LazyInstance = new Lazy<ForceResetEffect>(CreateInstance<ForceResetEffect>);
        public override bool CanStack { get; } = false;
        public override int Duration { get; } = 0;

        public override void FirstEffectApply(IUnitDataModificationService modificationService, IUnitDataRepository dataRepository, EffectSource source)
        {
            base.FirstEffectApply(modificationService, dataRepository, source);

            modificationService.ModifyHealth(0, source);
        }
    }
}