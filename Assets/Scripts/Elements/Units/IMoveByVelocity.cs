using UnityEngine;

namespace Elements.Units
{
    public interface IMoveByVelocity
    {
        float Acceleration { get; }
        float MaxSpeed { get; }
        Rigidbody Rigidbody { get; }
    }
}