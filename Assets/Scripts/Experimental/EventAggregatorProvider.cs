using Common.Class;
using EventManagement;
using UnityEngine;

namespace Experimental
{
    //if adapted scriptable paradigm, we will refactor this so every new scriptable instance hold a different event aggregator
    [CreateAssetMenu(menuName = "ScriptableService/EventAggregator")]
    public class EventAggregatorProvider : ScriptableObject
    {
        // ReSharper disable once MemberCanBeMadeStatic.Global
        public IEventAggregator ProvideEventAggregator() => EventAggregatorHolder.Instance;
    }
}