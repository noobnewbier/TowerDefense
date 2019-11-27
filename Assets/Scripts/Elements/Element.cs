using Common.Class;
using Common.Enum;
using Common.Event;
using Common.Interface;
using EventManagement;
using UnityEngine;

namespace Elements
{
    public abstract class Element : MonoBehaviour, IDynamicObjectOfInterest
    {
        protected IEventAggregator EventAggregator;
        protected virtual void OnEnable()
        {
            EventAggregator = EventAggregatorHolder.Instance;
            EventAggregator.Subscribe(this);
            
            EventAggregator.Publish(new DynamicObjectSpawnedEvent(this));
        }
        
        protected void OnDisable()
        {
            EventAggregator.Unsubscribe(this);
        }

        public abstract AiInterestCategory InterestCategory { get; }
        public abstract Bounds Bounds { get; }
    }
}