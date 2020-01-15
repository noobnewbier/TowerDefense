using Common.Enum;
using Effects.Modifiers;
using Elements.Units.UnitCommon;
using UnityEngine;

namespace Effects
{
    //Highly dangerous.... At some point, you will probably kill yourself because you wrote this
    public abstract class Effect : ScriptableObject
    {
        [SerializeField] protected Modifier modifier;
        public abstract bool CanStack { get; }
        
        public abstract int Duration { get; }

        public virtual void FirstEffectApply(IUnitDataModificationService modificationService, IUnitDataRepository dataRepository, EffectSource source)
        {
        }

        public virtual void TickEffect(IUnitDataModificationService modificationService, IUnitDataRepository dataRepository, EffectSource source)
        {
        }

        public virtual void EndEffect(IUnitDataModificationService modificationService, IUnitDataRepository dataRepository, EffectSource source)
        {
        }

        public EffectHandler CreateEffectHandler
            (IUnitDataModificationService modificationService, IUnitDataRepository dataRepository, EffectSource source) => new EffectHandler(this, dataRepository, modificationService, source);
    }
}