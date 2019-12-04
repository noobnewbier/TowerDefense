using Effects;

namespace Common.Event
{
    public struct ApplyEffectEvent
    {
        public Effect Effect { get; }

        public ApplyEffectEvent(Effect effect)
        {
            Effect = effect;
        }
    }
}