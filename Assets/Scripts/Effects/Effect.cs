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
        public abstract int Duration { get; }

        public virtual void FirstEffectApply(IUnitDataService service, IUnitDataRepository dataRepository, EffectSource source)
        {
        }

        public virtual void TickEffect(IUnitDataService service, IUnitDataRepository dataRepository, EffectSource source)
        {
        }

        public virtual void EndEffect(IUnitDataService service, IUnitDataRepository dataRepository, EffectSource source)
        {
        }

        public EffectHandler CreateEffectHandler
            (IUnitDataService service, IUnitDataRepository dataRepository, EffectSource source) => new EffectHandler(this, dataRepository, service, source);
    }
}