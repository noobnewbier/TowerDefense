using Common.Interface;
using ScriptableService;
using UnityEngine;

namespace AgentAi.Suicidal.TargetProviders
{
    public class PlayerTargetProvider : TargetProvider
    {
        [SerializeField] private PlayerInstanceTracker playerInstanceTracker;

        public override IDynamicObjectOfInterest Target => playerInstanceTracker.Player;
    }
}