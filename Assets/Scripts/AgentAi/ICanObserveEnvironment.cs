using UnityEngine;

namespace AgentAi
{
    public interface ICanObserveEnvironment
    {
        Texture2D GetObservation();
    }
}