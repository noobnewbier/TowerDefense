using System;
using Common;

namespace Movement
{
    public abstract class MovementInputSource
    {
        public abstract float Vertical();
        public abstract float Horizontal();

        public static MovementInputSource Of(Identity identity)
        {
            switch (identity)
            {
                case Identity.Player:
                    return new PlayerMovementInputSource();
                    break;
                case Identity.Ai:
                    return new AiMovementInputSource();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(identity), identity, null);
            }
        }
    }
}