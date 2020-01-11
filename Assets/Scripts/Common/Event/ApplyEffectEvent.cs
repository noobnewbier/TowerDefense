using Common.Enum;
using Common.Interface;
using Effects;

namespace Common.Event
{
    public struct ApplyEffectEvent
    {
        public Effect Effect { get; }
        public IEffectTaker EffectTaker { get; }
        public EffectSource Source { get; }

        public ApplyEffectEvent(Effect effect, IEffectTaker effectTaker, EffectSource source)
        {
            Effect = effect;
            EffectTaker = effectTaker;
            Source = source;
        }
    }
}