using UnityEngine;

namespace Elements.Units
{
    public interface IMoveByVelocity
    {
        float Acceleration { get; }
        float Deceleration { get; }
        float MaxSpeed { get; }
    }
}